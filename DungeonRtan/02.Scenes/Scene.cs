using DungeonRtan.UI;
using DungeonRtan.Objects;
using DungeonRtan.Manager;

namespace DungeonRtan.Scenes {
    public abstract class Scene : IGameComponent {
        protected ScreenUI mSceneUI;
        public Player? mPlayer { get; set; }

        public abstract bool Init();
        public abstract void Render();
        public abstract void Update();

        public T CreateUI<T>(bool bindClear = false) where T : ScreenUI, new() {
            mSceneUI = new T();

            mSceneUI.mOwner = this;
            
            Console.Clear();

            if(bindClear)
                InputManager.GetInst.ClearBind();

            if (!mSceneUI.Init())
                return null;

            return (T)mSceneUI;
        }
    }
}
