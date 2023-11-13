using DungeonRtan.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonRtan.Scenes {
    public class StartScene : Scene {

        public override bool Init() {
            CreateUI<StartUI>();
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
