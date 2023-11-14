using DungeonRtan.Scenes;
using DungeonRtan.Objects;

namespace DungeonRtan.Manager {

    internal class SceneManager : IGameComponent {

        private Scene? mScene;
        //private Scene? mNextScene;

        public bool Init() {
            CreateScene<MainScene>();
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

        public bool ChangeScene<T>(Player player) where T : Scene, new() {
            T newScene = new T();

            InputManager.GetInst.ClearBind();
            Console.Clear();

            mScene = newScene;
            newScene.mPlayer = player;

            if (!newScene.Init())
                return false;

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
