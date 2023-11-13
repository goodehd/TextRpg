using DungeonRtan.UI;
using DungeonRtan.Objects;

namespace DungeonRtan.Scenes {
    internal class MainScene : Scene {
        public override bool Init() {
            //mPlayer = new Player("테스트", 10, 5, 100);
            CreateUI<MainUI>();
            return true;
        }

        public override void Render() {
            mSceneUI.Render();
        }

        public override void Update() {
            mSceneUI.Update();
        }
    }
}
