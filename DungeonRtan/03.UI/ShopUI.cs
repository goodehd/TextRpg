
using DungeonRtan.Manager;
using DungeonRtan.Object;
using DungeonRtan.Scenes;
using System.Buffers.Text;

namespace DungeonRtan.UI {
    internal class ShopUI : ScreenUI {
        private List<string> shopTex;

        int baseX = 28;
        int baseY = 10;
        bool isBuy = true;

        public override bool Init() {
            base.Init();
            shopTex = new List<string> {
                "상점.",
                "원하는 행동을 선택해주세요",
                "",
                "1. 구매",
                "2. 판매",
            };

            InputManager.GetInst.AddBindFunction("Up", InputType.Down, UpKey);
            InputManager.GetInst.AddBindFunction("Down", InputType.Down, DownKey);
            InputManager.GetInst.AddBindFunction("Z", InputType.Down, Enter);
            InputManager.GetInst.AddBindFunction("X", InputType.Down, Back);

            PrintText(shopTex, baseX, baseY);

            Sym.X = baseX - 3;
            Sym.Y = baseY + 3;

            return true;
        }

        public override void Render() {
            PrintText(Sym.sym, Sym.X, Sym.Y);
        }

        public override void Update() {

        }

        private void UpKey() {
            if (isBuy)
                return;

            isBuy = !isBuy;
            SetSybPos(Sym.X, Sym.Y - 1);
        }

        private void DownKey() {
            if (!isBuy)
                return;

            isBuy = !isBuy;
            SetSybPos(Sym.X, Sym.Y + 1);
        }

        private void Enter() {
            if (isBuy) {
                mOwner.CreateUI<ItemsViewUI>(true).SetViewType(EItemViewType.ShopBuy, ((ShopScene)mOwner).items);
            } else {
                mOwner.CreateUI<ItemsViewUI>(true).SetViewType(EItemViewType.ShopSell, mOwner.mPlayer.Inven.Items);
            }
        }

        private void Back() {
            mOwner.CreateUI<MainUI>(true);
        }
    }
}
