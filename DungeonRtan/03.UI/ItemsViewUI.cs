
using DungeonRtan.Manager;
using DungeonRtan.Object;
using DungeonRtan.Objects;
using DungeonRtan.Scenes;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace DungeonRtan.UI {
    internal class ItemsViewUI : ScreenUI{
        private string titleTex;
        private List<string> category;
        private List<string> itemTypes;
        private List<string> itemNames;
        private List<string> itemAbils;
        private List<string> itemEx;
        private List<string> itemGold;

        private EItemViewType ViewType;
        private List<Item> items;
        private List<bool> isBuy;
        private int curIndex = 0;

        public override bool Init() {
            base.Init();

            category = new List<string>() {
                "[아이템 목록]",
                "타입       | 이름          | 능력치         | 설명                                  | 골드",
            };

            itemTypes = new List<string>();
            itemNames = new List<string>();
            itemAbils = new List<string>();
            itemEx = new List<string>();
            items = new List<Item>();
            itemGold = new List<string>();
            isBuy = new List<bool>();

            InputManager.GetInst.AddBindFunction("Up", InputType.Down, UpKey);
            InputManager.GetInst.AddBindFunction("Down", InputType.Down, DownKey);
            InputManager.GetInst.AddBindFunction("Z", InputType.Down, Enter);
            InputManager.GetInst.AddBindFunction("X", InputType.Down, Back);

            baseX = 5;
            baseY = 12;

            Sym.X = baseX - 3;
            Sym.Y = baseY + 2;

            return true;
        }

        public override void Render() {
            PrintText(Sym.sym, Sym.X, Sym.Y);
        }

        public override void Update() {

        }

        public void SetViewType(EItemViewType viewtype, List<Item> inven, EItemType itemType = EItemType.Max) {
            items.Clear();
            itemTypes.Clear();
            itemNames.Clear();
            itemAbils.Clear();
            itemEx.Clear();
            itemGold.Clear();
            isBuy.Clear();

            ViewType = viewtype;
            if (viewtype == EItemViewType.Equipped) {
                titleTex = "장착할 아이템을 선택해주세요. [E] : 착용중";
            } else if (viewtype == EItemViewType.View) { 
                titleTex = "보유 중인 아이템을 관리할 수 있습니다.";
            } else if (viewtype == EItemViewType.ShopBuy) {
                titleTex = $"구매할 아이템을 선택해주세요. [V] : 보유중, 골드 : {mOwner.mPlayer.Gold} G";
            } else if (viewtype == EItemViewType.ShopSell) {
                titleTex = $"판매할 아이템을 선택해주세요. [V] : 보유중, 골드 : {mOwner.mPlayer.Gold} G";
            }

            AddStrings(viewtype, inven, itemType);

            PrintText(new string(' ', Console.WindowWidth), baseX, baseY - 2);
            PrintText(titleTex, baseX, baseY - 2);
            PrintText(category, baseX, baseY);
            PrintText(itemTypes, baseX, baseY + 2);
            PrintText(itemNames, baseX + 11, baseY + 2);
            PrintText(itemAbils, baseX + 27, baseY + 2);
            PrintText(itemEx, baseX + 44, baseY + 2);
            PrintText(itemGold, baseX + 84, baseY + 2);
        }
        
        private void AddStrings(EItemViewType viewtype, List<Item> inven, EItemType itemType) {
            if (inven == null)
                return;

            for (int i = 0; i < inven.Count; ++i) {
                if ((itemType == EItemType.Max || inven[i].Type == itemType)) {
                    items.Add(inven[i]);
                    itemTypes.Add(GetTypeString(viewtype, inven[i], itemType));
                    itemNames.Add("| " + inven[i].Name);
                    itemAbils.Add("| " + inven[i].AbilDescrip);
                    itemEx.Add("| " + inven[i].Descrip);
                    itemGold.Add("| " + inven[i].Gold + " G");
                }
            }
        }

        private string GetTypeString(EItemViewType viewtype, Item item, EItemType itemType) {
            string name;
            switch (item.Type) {
                case EItemType.Weapon:
                    name = "무기";
                    break;
                case EItemType.Armor:
                    name = "방어구";
                    break;
                default:
                    return "몰?루";
            }

            if (item.isEquipped && 
                (viewtype == EItemViewType.Equipped || viewtype == EItemViewType.ShopSell))
                name += "[E]";

            bool buy = false;
            if (mOwner.mPlayer.Inven.Items.Find(x => x.Name == item.Name) != default &&
                viewtype == EItemViewType.ShopBuy) {
                name += "[V]";
                buy = true;
            }

            isBuy.Add(buy);

            return name;
        }

        private void UpKey() {
            if (curIndex == 0)
                return;

            curIndex--;
            SetSybPos(Sym.X, Sym.Y - 1);
        }

        private void DownKey() {
            if (curIndex >= items.Count - 1)
                return;

            curIndex++;
            SetSybPos(Sym.X, Sym.Y + 1);
        }

        private void Enter() {
            if (items.Count == 0)
                return;

            switch (ViewType) {
                case EItemViewType.Equipped:
                    if (items[curIndex].isEquipped) {
                        mOwner.mPlayer.Inven.Unequip(items[curIndex].Type);
                    } else {
                        mOwner.mPlayer.Inven.ChangeCurItem(items[curIndex]);
                    }
                    mOwner.CreateUI<itmChgUI>(true);
                    break;
                case EItemViewType.View:
                    break; ;
                case EItemViewType.ShopBuy:
                    ((ShopScene)mOwner).BuyItem(curIndex, isBuy[curIndex]);
                    break;
                case EItemViewType.ShopSell:
                    ((ShopScene)mOwner).SellItem(items[curIndex]);
                    break;
                default:
                    break;
            }
        }

        private void Back() {
            switch (ViewType) {
                case EItemViewType.Equipped:
                    mOwner.CreateUI<itmChgUI>(true);
                    break;
                case EItemViewType.View:
                    break;
                case EItemViewType.ShopBuy:
                case EItemViewType.ShopSell:
                    mOwner.CreateUI<ShopUI>(true);
                    break;
                case EItemViewType.Max:
                    break;
                default:
                    break;
            }
        }
    }
}