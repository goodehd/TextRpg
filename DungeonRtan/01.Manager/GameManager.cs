
namespace DungeonRtan.Manager {
    internal class GameManager {
        private Resolution mScreen;
        private PlatformID mCurPlatform;

        public Resolution GetResolution() {
            return mScreen;
        }

        // 콘솔 윈도우 사이즈와 게임의 매니저들을 초기화 하는 함수
        public bool Init() {
            Console.CursorVisible = false;

            mScreen.Width = 100;
            mScreen.Height = 30;

            // Console.SetWindowSize()가 windows 운영체제에서만 돌아가기 때문에 실행하고 있는
            // 컴퓨터의 운영체제를 체크함
            OperatingSystem os = Environment.OSVersion;
            mCurPlatform = os.Platform;

            if(mCurPlatform == PlatformID.Win32NT) {
                Console.SetWindowSize(mScreen.Width, mScreen.Height);
            } else if(mCurPlatform == PlatformID.MacOSX){
                
            }

            if (!InputManager.GetInst.Init())
                return false;

            if (!SceneManager.GetInst.Init())
                return false;

            return true;
        }

        public void Run() {
            /*
             전채적인 게임 로직 :
             1.키 입력처리
             2.키 입력 처리를 바탕으로 객체 데이터 업데이트
             3.업데이트 된 데이터를 바탕으로 콘솔 글자 수정
            */

            while (true) {
                // 콘솔 위도우 크기 고정
                if (mCurPlatform == PlatformID.Win32NT)
                    PixWindowSize();

                Update();

                Render();
            }
        }

        public void Update() {
            InputManager.GetInst.Update();
            SceneManager.GetInst.Update();
        }

        public void Render() {
            SceneManager.GetInst.Render();
        }

        private void PixWindowSize() {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            if(mScreen.Width < width || mScreen.Height < height)
                Console.SetWindowSize(mScreen.Width, mScreen.Height);
        }

        //싱글톤 패턴
        private static GameManager instance;

        private GameManager() { }

        public static GameManager GetInst {
            get {
                if (instance == null) {
                    instance = new GameManager();
                }
                return instance;
            }
        }
    }
}
