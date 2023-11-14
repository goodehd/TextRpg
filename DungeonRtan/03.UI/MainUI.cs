
using DungeonRtan.Manager;
using DungeonRtan.Scenes;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using static DungeonRtan.Scenes.CharCreateScene;

namespace DungeonRtan.UI {
    internal class MainUI : ScreenUI {
        enum EMainMenu {
            Status = 12,
            Inven,
            Shop,
            Dungeon,
            Rest,
            Save,
            Exit,
            Max
        }
        private List<String> mainMenu;
        private EMainMenu curMenu = EMainMenu.Status;

        public override bool Init() {
            base.Init();
            
            mainMenu = new List<String>() {
                "스파르타 마을에 오신 여러분 환영합니다.",
                "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.",
                " ",
                " ",
                "                1. 상태 보기",
                "                2. 인벤토리",
                "                3. 상점",
                "                4. 던전 입장",
                "                5. 휴식",
                "                6. 저장하기",
                "                7. 종료"
            };

            InputManager.GetInst.AddBindFunction("Down", InputType.Down, MoveDownSyb);
            InputManager.GetInst.AddBindFunction("Up", InputType.Down, MoveUpSyb);
            InputManager.GetInst.AddBindFunction("Z", InputType.Down, Enter);

            int X = 27;
            int Y = 8;

            PrintText(mainMenu, X, Y);
            
            Sym.X = X + 13;
            Sym.Y = Y + 4;
            PrintText(Sym.sym, Sym.X, Sym.Y);

            return true;
        }

        public override void Render() {
            PrintText(Sym.sym, Sym.X, Sym.Y);
        }
        
        public override void Update() {

        }

        private void MoveDownSyb() {
            int y = Sym.Y == (int)EMainMenu.Max - 1 ? Sym.Y : Sym.Y + 1;
            curMenu = curMenu == EMainMenu.Max - 1 ? curMenu : curMenu + 1;
            SetSybPos(Sym.X, y);
        }

        private void MoveUpSyb() {
            int y = Sym.Y == (int)EMainMenu.Status ? Sym.Y : Sym.Y - 1;
            curMenu = curMenu == EMainMenu.Status ? curMenu : curMenu - 1;
            SetSybPos(Sym.X, y);
        }

        private void Enter() {
            switch (curMenu) {
                case EMainMenu.Status:
                    mOwner.CreateUI<StatusViewUI>(true);
                    break;
                case EMainMenu.Inven:
                    mOwner.CreateUI<itmChgUI>(true);
                    break;
                case EMainMenu.Shop:
                    SceneManager.GetInst.ChangeScene<ShopScene>(mOwner.mPlayer);
                    break;
                case EMainMenu.Dungeon:
                    break;
                case EMainMenu.Rest:
                    break;
                case EMainMenu.Save:
                    break;
                case EMainMenu.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    throw new InvalidOperationException("올바르지 않은 접근");
            }
        }
    }
}

