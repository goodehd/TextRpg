
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
                status = new List<string> {
                 "        상태 보기",
                 "캐릭터의 정보가 표시됩니다",
                 "",
                $"       Lv. {Player.Level}",
                $"       class : {Player.Chad.ToString()}",
                $"       공격력 : {Player.ATK}",
                $"       방어력 : {Player.DEF}",
                $"       체력 : {Player.HP}",
                $"       Gold : {Player.Gold} G",
                "",
                "         나가기"
                };

                PrintText(status, 38, 8);
            }
            InputManager.GetInst.AddBindFunction("Z", InputType.Down, Enter);

            Sym.X = 45;
            Sym.Y = 18;

            return true;
        }

        public override void Render() {
            PrintText(Sym.sym, Sym.X, Sym.Y);
        }

        public override void Update() {
            
        }

        public void Enter() {
            mOwner.CreateUI<MainUI>();
        }
    }
}
