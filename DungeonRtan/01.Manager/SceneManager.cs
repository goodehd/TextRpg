using DungeonRtan.Scenes;
using DungeonRtan.Objects;

namespace DungeonRtan.Manager {

    internal class SceneManager : IGameComponent {

        private Scene? mScene;
        //private Scene? mNextScene;

        public bool Init() {
            CreateScene<StartScene>();
            return true;
        }

        public void Update() {
            mScene.Update();
        }

        public void Render() {
            mScene.Render();
        }

        private bool CreateScene<T>() where T : Scene, new() {
            T newScene = new T();

            if (!newScene.Init())
                return false;

            mScene = newScene;

            return true;
        }

        public void ChangeScene<T>() where T : Scene, new() {
            T nextScene = new T();

            InputManager.GetInst.ClearBind();
            Console.Clear();

            nextScene.Init();

            mScene = nextScene;
        }

        public bool ChangeScene<T>(Player player) where T : Scene, new() {
            T newScene = new T();

            InputManager.GetInst.ClearBind();
            Console.Clear();

            if (!newScene.Init())
                return false;

            mScene = newScene;
            newScene.mPlayer = player;

            return true;
        }

        private static SceneManager instance;

        private SceneManager() { }

        public static SceneManager GetInst {
            get {
                if (instance == null) {
                    instance = new SceneManager();
                }
                return instance;
            }
        }
    }
}
