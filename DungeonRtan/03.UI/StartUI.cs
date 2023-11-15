
using DungeonRtan.Manager;
using DungeonRtan.Objects;
using DungeonRtan.Scenes;
using System.Buffers.Text;
using System.Reflection.Metadata.Ecma335;

namespace DungeonRtan.UI {
    internal class StartUI : ScreenUI {
        // scene에서 사용하는 텍스트들.
        private List<String> title;
        private List<String> titleMenu;

        // 어느 메뉴를 선택했는지 확인하는 enum.
        private EStartMenu curMenu = EStartMenu.Start;

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

            // 전체적인 글자가 함께 움직이게 하기 위해 설정하는 기본적인 좌표
            baseX = 12;
            baseY = 5;

            // senenUI에서 사용하는 함수들을 각 키에 맞게 등록.
            InputManager.GetInst.AddBindFunction("Up", InputType.Down, UpKey);
            InputManager.GetInst.AddBindFunction("Down", InputType.Down, DownKey);
            InputManager.GetInst.AddBindFunction("Z", InputType.Down, Enter);

            PrintText(title, baseX, baseY);
            PrintText(titleMenu, baseX + 30, baseY + 15);

            Sym.X = baseX + 27;
            Sym.Y = baseY + 15;

            return true;
        }

        public override void Render() {
            PrintText(Sym.sym, Sym.X, Sym.Y);
        }

        public override void Update() {
        
        }

        private void UpKey() {
            // 현재 가리키고 있는 메뉴가 첫 메뉴일경우 예외처리
            if (curMenu == EStartMenu.Start)
                return;

            curMenu--;
            SetSybPos(Sym.X, Sym.Y - 1);
        }

        private void DownKey() {
            // 현재 가리키고 있는 메뉴가 마지막 메뉴일 경우 예외처리
            if (curMenu == EStartMenu.Max - 1)
                return;

            // 가리키고 있는 메뉴와 화살표 위치 수정
            curMenu++;
            SetSybPos(Sym.X, Sym.Y + 1);
        }

        private void Enter() {
            switch (curMenu) {
                case EStartMenu.Start:
                    // CharCreateScene으로 변환
                    SceneManager.GetInst.ChangeScene<CharCreateScene>(null);
                    break;
                case EStartMenu.Continue:
                    // 로드 진행
                    ContinuePlay();
                    break;
                case EStartMenu.Exit:
                    // 종료
                    Environment.Exit(0);
                    break;
                default:
                    throw new InvalidOperationException("올바르지 않은 접근");
            }
        }

        private void ContinuePlay() {
            // 현재 프로젝트 실행 파일이 있는 폴더 기준으로 경로를 받아온다.
            string projectPath = Directory.GetCurrentDirectory();
            // bin 폴더를 기준으로 문자열 파싱
            string[] pathSegments = projectPath.Split(new[] { "bin" }, StringSplitOptions.None);
            // 경로를 완성시켜준다.
            projectPath = pathSegments[0] + "Save\\player.bin";

            //파일이 없을 경우 예외처리
            if (!File.Exists(projectPath))
                return;

            // 로드 진행
            Player player = new Player();
            player.Load();
            SceneManager.GetInst.ChangeScene<MainScene>(player);
        }
    }
}
