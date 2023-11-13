﻿using DungeonRtan.UI;
using DungeonRtan.Objects;
using DungeonRtan.Manager;

namespace DungeonRtan.Scenes {
    public abstract class Scene : IGameComponent {
        protected ScreenUI mSceneUI;
        public Player? mPlayer { get; set; }

        public abstract bool Init();
        public abstract void Render();
        public abstract void Update();

        public bool CreateUI<T>() where T : ScreenUI, new() {
            mSceneUI = new T();

            mSceneUI.mOwner = this;
            
            Console.Clear();
            InputManager.GetInst.ClearBind();

            if (!mSceneUI.Init())
                return false;

            return true;
        }
    }
}
