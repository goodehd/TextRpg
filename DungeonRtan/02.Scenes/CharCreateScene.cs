using DungeonRtan.Manager;
using DungeonRtan.Objects;
using DungeonRtan.UI;

namespace DungeonRtan.Scenes {
    internal class CharCreateScene : Scene {
        public enum Step {
            InputName,
            ClassSelect,
            StatusSelect,
            End
        }

        public enum Status {
            Atk = 16,
            Def,
            Hp,
            End
        }

        public string name { get; private set; }
        public int BaseAtk { get; private set; } = 10;
        public int BaseDef { get; private set; } = 5;
        public int BaseHp { get; private set; } = 100;
        public int BasePoint { get; private set; } = 10;

        public CharClass SelectClass { get; set; } = CharClass.Warrior;
        public Step CurStep { get; set; } = Step.InputName;
        public Status CurStatus { get; set; } = Status.Atk;

        public override bool Init() {

            InputManager.GetInst.AddBindFunction("Z", InputType.Down, ZPush);
            InputManager.GetInst.AddBindFunction("X", InputType.Down, XPush);

            CreateUI<CharCreateUI>();

            return true;
        }

        public override void Render() {
            mSceneUI.Render();
        }

        public override void Update() { 
            if(CurStep == Step.InputName)
                InputName();

            mSceneUI.Update();
        }

        private void InputName() {
            Console.CursorVisible = true;
            Console.SetCursorPosition(34, 8);
            Console.Write("                          ");

            Console.SetCursorPosition(34, 8);
            name = Console.ReadLine();

            CurStep++;
            Console.CursorVisible = false;
        }

        private void ZPush() {
            if (CurStep == Step.End) {
                if (mSceneUI.Sym.X == 37) {
                    Player player = new Player(name, BaseAtk, BaseDef, BaseHp);
                    player.Inven.Init();
                    SceneManager.GetInst.ChangeScene<MainScene>(player);
                } else {
                    SceneManager.GetInst.ChangeScene<StartScene>(null);
                }
            }

            if (CurStep == Step.StatusSelect && BasePoint > 0) {
                switch (CurStatus) {
                    case Status.Atk:
                        BaseAtk++;
                        break;
                    case Status.Def:
                        BaseDef++;
                        break;
                    case Status.Hp:
                        BaseHp += 5;
                        break;
                    default:
                        throw new InvalidOperationException("올바르지 않은 접근");
                }
                BasePoint--;
            }
        }

        private void XPush() {
            if (CurStep == Step.StatusSelect && BasePoint < 10) {
                switch (CurStatus) {
                    case Status.Atk:
                        if (BaseAtk > 10) {
                            BasePoint++;
                            BaseAtk--;
                        }
                        break;
                    case Status.Def:
                        if (BaseDef > 5) {
                            BasePoint++;
                            BaseDef--;
                        }
                        break;
                    case Status.Hp:
                        if (BaseHp > 100) {
                            BasePoint++;
                            BaseHp -= 5;
                        }
                        break;
                    default:
                        throw new InvalidOperationException("올바르지 않은 접근");
                }
            }
        }
    }
}
