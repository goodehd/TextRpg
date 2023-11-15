using DungeonRtan.UI;
using DungeonRtan.Objects;
using DungeonRtan.Manager;

namespace DungeonRtan.Scenes {
    public abstract class Scene : IGameComponent {
        protected ScreenUI mSceneUI;            //scene에서 사용하는 UI
        public Player? mPlayer { get; set; }    //scene에서 사용되는 player

        public abstract bool Init();
        public abstract void Render();
        public abstract void Update();

        // 사용하기 위한 UI를 생성하는 함수
        public T CreateUI<T>(bool bindClear = false) where T : ScreenUI, new() {
            mSceneUI = new T();

            mSceneUI.mOwner = this;
            
            Console.Clear();

            // 키 초기화가 필요할 경우 초기화 한다.
            if(bindClear)
                InputManager.GetInst.ClearBind();

            if (!mSceneUI.Init())
                return null;

            return (T)mSceneUI;
        }
    }
}
