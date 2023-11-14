
using DungeonRtan.Manager;
using DungeonRtan.Object;
using DungeonRtan.Objects;
using DungeonRtan.Scenes;
using System.Diagnostics;

namespace DungeonRtan.UI {
    internal class itmChgUI : ScreenUI {
        private List<string> Equipments;
        private List<string> EquipmentNames;
        private List<string> EquipmentAbility;
        private EItemType curType = EItemType.Weapon;

        public override bool Init() {
            base.Init();

            Equipments = new List<string>() {
                "장비창",
                "장비하고 있는 아이템 입니다. 변경하고 싶은 아이템을 선택하세요.",
                "",
                "|타입         | 이름               | 능력치",
                "|무기         |                    |",
                "|방어구       |                    |"
            };

            EquipmentNames = new List<string>();
            EquipmentAbility = new List<string>();

            InputManager.GetInst.AddBindFunction("Up", InputType.Down, UpKey);
            InputManager.GetInst.AddBindFunction("Down", InputType.Down, DownKey);
            InputManager.GetInst.AddBindFunction("Z", InputType.Down, Enter);
            InputManager.GetInst.AddBindFunction("X", InputType.Down, Back);

            AddEquipmentInfo();
            PrintText();

            return true; 
        }

        public override void Render() {
            PrintText(Sym.sym, Sym.X, Sym.Y);
        }

        public override void Update() {
            
        }

        public void AddEquipmentInfo() {
            Player player = mOwner.mPlayer;
            Inventory inventory = player.Inven;

            string weponeName = inventory.CurWeapon == null ? "없음" : inventory.CurWeapon.Name;
            string ArmorName = inventory.CurArmor == null ? "없음" : inventory.CurArmor.Name;

            EquipmentNames.Add(weponeName);
            EquipmentNames.Add(ArmorName);

            string weponeAbil = inventory.CurWeapon == null ? "없음" : "공격력 : +" + inventory.CurWeapon.ATK.ToString();
            string ArmorAbil = inventory.CurArmor == null ? "없음" : "방어력 : +" + inventory.CurArmor.DEF.ToString();

            EquipmentAbility.Add(weponeAbil);
            EquipmentAbility.Add(ArmorAbil);
        }

        private void PrintText() {
            int baseX = 20;
            int baseY = 10;
            PrintText(Equipments, baseX, baseY);
            PrintText(EquipmentNames, baseX + 16, baseY + 4);
            PrintText(EquipmentAbility, baseX + 37, baseY + 4);
            SetSybPos(baseX - 2, baseY + 4);
        }

        private void UpKey() {
            int y = curType == EItemType.Weapon ? Sym.Y : Sym.Y - 1;
            curType = curType == EItemType.Weapon ? curType : curType - 1;
            SetSybPos(Sym.X, y);
        }

        private void DownKey() {
            int y = curType == EItemType.Max - 1 ? Sym.Y : Sym.Y + 1;
            curType = curType == EItemType.Max - 1 ? curType : curType + 1;
            SetSybPos(Sym.X, y);
        }

        private void Enter() {
            switch (curType) {
                case EItemType.Weapon:
                case EItemType.Armor:
                    mOwner.CreateUI<ItemsViewUI>(true).SetViewType(EItemViewType.Equipped,
                        mOwner.mPlayer.Inven.Items, curType);
                    break;
                case EItemType.Max:
                    SceneManager.GetInst.ChangeScene<MainScene>(mOwner.mPlayer);
                    break;
                default:
                    break;
            }
        }

        private void Back() {
            SceneManager.GetInst.ChangeScene<MainScene>(mOwner.mPlayer);
        }
    }
}
