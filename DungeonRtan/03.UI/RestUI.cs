
using DungeonRtan.Manager;
using DungeonRtan.Scenes;
using System.Diagnostics.Metrics;

namespace DungeonRtan.UI {
    internal class RestUI : ScreenUI {
        private List<string> restTex;
        private string successTex = "                    휴식을 완료했습니다";
        private string failTex = "                       Gold가 부족합니다.";

        private int baseX = 20;
        private int baseY = 10;

        private bool isZpush = false;

        public override bool Init() {
            base.Init();

            restTex = new List<string>() {
                "휴식하기",
                "500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : 800 G)",
                "",
                "Z : 휴식하기",
                "X : 나가기"
            };

            InputManager.GetInst.AddBindFunction("Z", InputType.Down, Enter);
            InputManager.GetInst.AddBindFunction("X", InputType.Down, Back);

            PrintText(restTex, baseX, baseY);

            return true;
        }

        private void Enter() {
            if (!isZpush) {
                Console.Clear();
                if (mOwner.mPlayer.Gold > 500) {
                    PrintText(successTex, baseX, baseY);
                    mOwner.mPlayer.HP += 50;
                    if (mOwner.mPlayer.HP > mOwner.mPlayer.MaxHP)
                        mOwner.mPlayer.HP = mOwner.mPlayer.MaxHP;
                    isZpush = true;
                } else {
                    PrintText(failTex, baseX, baseY);
                }
            } else {
                mOwner.CreateUI<MainUI>(true);
            }
        }

        private void Back() {
            mOwner.CreateUI<MainUI>(true);
        }

        public override void Render() { }
        public override void Update() { }
    }
}
