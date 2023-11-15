using DungeonRtan.Scenes;
using DungeonRtan.Objects;

namespace DungeonRtan.Manager {

    internal class SceneManager : IGameComponent {

        private Scene? mScene;

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

        // 초기 scene을 생성하는 함수
        // where T : 이 함수에 들어올 수 있는 타입을 제한하는 문법, 여기서는 scnen을 상속 받는 타입만 사용가능
        private bool CreateScene<T>() where T : Scene, new() {
            T newScene = new T();

            if (!newScene.Init())
                return false;

            mScene = newScene;

            return true;
        }

        // scene 전환을 하기 위한 함수 
        // where T : 이 함수에 들어올 수 있는 타입을 제한하는 문법, 여기서는 scnen을 상속 받는 타입만 사용가능
        // player를 넘겨 받아 생성된 player를 돌려 쓴다.
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
