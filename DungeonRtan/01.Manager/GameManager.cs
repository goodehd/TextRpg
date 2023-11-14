
namespace DungeonRtan.Manager {
    internal class GameManager {
        private Resolution mScreen;
        private PlatformID mCurPlatform;

        public Resolution GetResolution() {
            return mScreen;
        }

        public bool Init() {
            Console.CursorVisible = false;

            mScreen.Width = 100;
            mScreen.Height = 30;

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
            while (true) {
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
