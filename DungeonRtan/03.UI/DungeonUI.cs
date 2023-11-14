using DungeonRtan.Manager;
using DungeonRtan.Object;
using DungeonRtan.Objects;
using DungeonRtan.Scenes;
using System.Diagnostics.Metrics;

namespace DungeonRtan.UI {
    internal class DungeonUI : ScreenUI {
        private List<string> dungeonTitle;
        private EDifficulty selectDiff = EDifficulty.Easy;
        private bool isResult = false;

        private int[] OptimalDefense = new int[(int)EDifficulty.Max]{5, 11, 17};
        private int[] DungeonGold = new int[(int)EDifficulty.Max] { 1000, 1700, 2500 };

        int baseX = 28;
        int baseY = 10;

        public override bool Init() {
            base.Init();
            dungeonTitle = new List<string> {
                "던전입장.",
                "이곳에서 던전으로 들어가기전 활동을 할 수 있습니다",
                "",
                "1. 쉬운 던전       | 방어력 5 이상 권장",
                "2. 일반 던전       | 방어력 11 이상 권장",
                "3. 어려운 던전     | 방어력 17 이상 권장",
            };

            InputManager.GetInst.AddBindFunction("Up", InputType.Down, UpKey);
            InputManager.GetInst.AddBindFunction("Down", InputType.Down, DownKey);
            InputManager.GetInst.AddBindFunction("Z", InputType.Down, Enter);
            InputManager.GetInst.AddBindFunction("X", InputType.Down, Back);

            PrintText(dungeonTitle, baseX, baseY);

            Sym.X = baseX - 3;
            Sym.Y = baseY + 3;

            return true;
        }

        public override void Render() {
            if(!isResult)
                PrintText(Sym.sym, Sym.X, Sym.Y);
        }

        public override void Update() {

        }

        private void UpKey() {
            if (selectDiff == EDifficulty.Easy)
                return;

            selectDiff--;
            SetSybPos(Sym.X, Sym.Y - 1);
        }

        private void DownKey() {
            if (selectDiff == EDifficulty.Max - 1)
                return;

            selectDiff++;
            SetSybPos(Sym.X, Sym.Y + 1);
        }

        private void Enter() {
            if (!isResult)
                CalculateResult();
            else
                mOwner.CreateUI<MainUI>(true);
        }

        private void Back() {
            mOwner.CreateUI<MainUI>(true);
        }

        private void CalculateResult() {
            Random random = new Random();
            double failureProbability = 0.4;

            int playerAtk = mOwner.mPlayer.ATK;
            int playerDef = mOwner.mPlayer.DEF;
            int playerHp = mOwner.mPlayer.HP;
            int dungeonDef = OptimalDefense[(int)selectDiff];

            if (playerDef < dungeonDef && random.NextDouble() < failureProbability) {
                mOwner.mPlayer.HP -= playerHp / 2;
                PrintResult(false, false, playerHp / 2, 0);
                return;
            }

            int lossHp = random.Next(20, 36);
            lossHp -= dungeonDef - playerDef;
            mOwner.mPlayer.HP -= lossHp;

            int minAtkRange = playerAtk;
            int maxAtkRange = playerAtk * 2;

            float CompGold = DungeonGold[(int)selectDiff] +
                DungeonGold[(int)selectDiff] * random.Next(minAtkRange, maxAtkRange + 1) / 100.0f;
            mOwner.mPlayer.Gold += (int)CompGold;

            bool levelup = false;
            mOwner.mPlayer.ClearCount++;
            if(mOwner.mPlayer.Level == mOwner.mPlayer.ClearCount) {
                mOwner.mPlayer.Level++;
                mOwner.mPlayer.ClearCount = 0;
                mOwner.mPlayer.ATK += 1;
                mOwner.mPlayer.DEF += 2;
                levelup = true;
            }

            PrintResult(true, levelup, lossHp, (int)CompGold);
        }

        private void PrintResult(bool isClear, bool levelup, int lossHP, int compGold) {
            Console.Clear();
            isResult = true;

            string text;
            if (isClear) {
                text = "던전 클리어";
            } else {
                text = "던전 클리어 실패";
            }

            int up = 0;
            if(levelup) {
                up = 1;
            }

            List<string> result = new List<string> {
                text,
                "",
                "[탐험 결과]",
                $"Lv {mOwner.mPlayer.Level - up} -> {mOwner.mPlayer.Level} (+{up})",
                $"체력 {mOwner.mPlayer.HP + lossHP} -> {mOwner.mPlayer.HP} ({-lossHP})",
                $"Gold {mOwner.mPlayer.Gold - compGold} -> {mOwner.mPlayer.Gold} (+{compGold})",
                "",
                "나가기 : Z, X"
            };

            PrintText(result, baseX, baseY);
        }
    }
}
