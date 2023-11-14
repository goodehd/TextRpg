
using DungeonRtan.Manager;
using DungeonRtan.Objects;
using DungeonRtan.Scenes;

namespace DungeonRtan.UI {
    internal class StatusViewUI : ScreenUI {
        private List<string> status; 

        public override bool Init() {
            base.Init();
            Player Player = mOwner.mPlayer;
            if (Player != null) {
                int weponeAtk = Player.Inven.CurWeapon == null ? 0 : Player.Inven.CurWeapon.ATK;
                int armorDef = Player.Inven.CurArmor == null ? 0 : Player.Inven.CurArmor.DEF;

                status = new List<string> {
                 "상태 보기",
                 "캐릭터의 정보가 표시됩니다",
                 "",
                $"이름 : {Player.Name}",
                $"Lv. {Player.Level}",
                $"class : {Player.Chad.ToString()}",
                $"공격력 : {Player.ATK} (+{weponeAtk})",
                $"방어력 : {Player.DEF} (+{armorDef})",
                $"체력 : {Player.HP}",
                $"Gold : {Player.Gold} G",
                };

                PrintText(status, 38, 8);
            }
            InputManager.GetInst.AddBindFunction("X", InputType.Down, Back);

            return true;
        }

        public override void Render() {

        }

        public override void Update() {
            
        }

        public void Back() {
            mOwner.CreateUI<MainUI>(true);
        }
    }
}
