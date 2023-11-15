using DungeonRtan.Object;
using DungeonRtan.Objects;
using DungeonRtan.UI;

namespace DungeonRtan.Scenes {
    internal class ShopScene : Scene {
        public List<Item> items { get; private set; }

        public override bool Init() {

            // 상점에서 팔매할 아이템 리스트
            items = new List<Item>();
            items.Add(new Item("막대기", 5, 0, 100, 0, EItemType.Weapon, "마악대기이이이이", false, "공격력 : +5"));
            items.Add(new Item("돌맹이", 5, 0, 100, 0, EItemType.Weapon, "도올매앵이이이잉", false, "공격력 : +5"));
            items.Add(new Item("패딩", 5, 0, 100, 0, EItemType.Weapon, "패딩은 겨울 필수품이에요", false, "공격력 : +5"));
            items.Add(new Item("양말", 5, 0, 100, 0, EItemType.Weapon, "발이 너무 시려워요", false, "공격력 : +5"));
            
            items.Add(new Item("모자", 0, 2, 100, 0, EItemType.Armor, "모오오오오오오오옹자", false, "방어력 : +2"));
            items.Add(new Item("바지", 0, 2, 100, 0, EItemType.Armor, "바바아앙방바앙비지", false, "방어력 : +2"));
            items.Add(new Item("고양이", 0, 10, 1000, 0, EItemType.Armor, "나만 고양이 없는 것 같아요", false, "방어력 : +10"));
            items.Add(new Item("윤하의 벨트", 0, 1, 10000, 0, EItemType.Armor, "터져버린 흔적이 보여요", false, "방어력 : +1"));

            CreateUI<ShopUI>(true);
            return true;
        }

        public override void Render() {
            mSceneUI.Render();
        }

        public override void Update() {
            mSceneUI.Update();
        }

        // 아이템 구매 함수
        public void BuyItem(int index, bool isBuy) {
            // 구매한 아이템일 경우 예외처리
            if (isBuy)
                return;

            // 돈부족
            if (mPlayer.Gold < items[index].Gold)
                return;

            mPlayer.Inven.AddItem(items[index]);
            mPlayer.Gold -= items[index].Gold;
            ((ItemsViewUI)mSceneUI).SetViewType(EItemViewType.ShopBuy, items);
        }

        // 아이템 팥매 함수
        public void SellItem(Item item) {
            mPlayer.Gold += (int)(item.Gold * 0.85f);
            mPlayer.Inven.Items.Remove(item);

            if (mPlayer.Inven.CurWeapon == item)
                mPlayer.Inven.CurWeapon = null;

            if (mPlayer.Inven.CurArmor == item)
                mPlayer.Inven.CurArmor = null;

            Console.Clear();
            ((ItemsViewUI)mSceneUI).SetViewType(EItemViewType.ShopSell, mPlayer.Inven.Items);
        }
    }
}
