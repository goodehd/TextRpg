
using DungeonRtan.Manager;
using DungeonRtan.Scenes;
using System.Reflection.Metadata.Ecma335;

namespace DungeonRtan.UI {
    internal class StartUI : ScreenUI {
        enum StartMenu {
            Start = 20,
            Continue,
            Exit,
            Max
        }

        private List<String> title;
        private List<String> titleMenu;
        private int sybX;
        private StartMenu sybY;

        public override bool Init() {
            base.Init();
            title = new List<string> {
                "______                                             ______  _                 ",
                "|  _  \\                                            | ___ \\| |                ",
                "| | | | _   _  _ __    __ _   ___   ___   _ __     | |_/ /| |_   __ _  _ __  ",
                "| | | || | | || '_ \\  / _` | / _ \\ / _ \\ | '_ \\    |    / | __| / _` || '_ \\ ",
                "| |/ / | |_| || | | || (_| ||  __/| (_) || | | |   | |\\ \\ | |_ | (_| || | | |",
                "|___/   \\__,_||_| |_| \\__, | \\___| \\___/ |_| |_|   \\_| \\_| \\__| \\__,_||_| |_|",
                "                       __/ |                                                 ",
                "                      |___/                                                  "
            };

            titleMenu = new List<String>() {
                "1. 새로 시작",
                "2. 이어서 시작",
                "3. 종료"
            };

            sybX = 39;
            sybY = StartMenu.Start;

            InputManager.GetInst.AddBindFunction("Down", InputType.Down, MoveDownSyb);
            InputManager.GetInst.AddBindFunction("Up", InputType.Down, MoveUpSyb);
            InputManager.GetInst.AddBindFunction("Z", InputType.Down, Enter);

            return true;
        }

        public override void Render() {
            PrintText(title, 12, 5);
            PrintText(titleMenu, 42, 20);
            PrintText(Sym.sym, sybX, (int)sybY);
        }

        public override void Update() {
        
        }

        private void MoveDownSyb() {
            Console.SetCursorPosition(sybX, (int)sybY);
            Console.WriteLine("  ");
            sybY = sybY == StartMenu.Exit ? StartMenu.Start : sybY + 1;
        }

        private void MoveUpSyb() {
            Console.SetCursorPosition(sybX, (int)sybY);
            Console.WriteLine("  ");
            sybY = sybY == StartMenu.Start ? StartMenu.Exit : sybY - 1;
        }

        private void Enter() {
            switch (sybY) {
                case StartMenu.Start:
                    SceneManager.GetInst.ChangeScene<CharCreateScene>(null);
                    break;
                case StartMenu.Continue:
                    break;
                case StartMenu.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    throw new InvalidOperationException("올바르지 않은 접근");
            }
        }
    }
}
