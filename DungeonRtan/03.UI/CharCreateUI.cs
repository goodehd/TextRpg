using DungeonRtan.Manager;
using DungeonRtan.Scenes;
using System.Xml.Linq;
using static DungeonRtan.Scenes.CharCreateScene;

namespace DungeonRtan.UI  {
    internal class CharCreateUI : ScreenUI {
        private List<string>? inputName;
        private List<string>? classSelect;
        private List<string>? statusSelect;
        private string? select;

        public override bool Init() {
            base.Init();
            inputName = new List<string>() {
                "1. 캐릭터의 이름을 입력해 주세요.",
                ">> "
            };

            classSelect = new List<string>() {
                "2. 직업을 선택해 주세요.",
                "   전사      마법사"
            };

            CharCreateScene Scene = (CharCreateScene)mOwner;
            statusSelect = new List<string> {
                "3. 스탯 분배를 해주세요. (남은 포인트 : " + Scene.BasePoint.ToString("D2") + ")",
                "  공격력 : " + Scene.BaseAtk,
                "  방어력 : " + Scene.BaseDef,
                "  체력   : " + Scene.BaseHp
            };

            select = "확인      취소";

            PrintText(inputName, 31, 7);
            PrintText(classSelect, 31, 11);
            PrintText(statusSelect, 31, 15);
            PrintText(select, 39, 21);

            InputManager.GetInst.AddBindFunction("Down", InputType.Down, MoveDownSyb);
            InputManager.GetInst.AddBindFunction("Up", InputType.Down, MoveUpSyb);
            InputManager.GetInst.AddBindFunction("Right", InputType.Down, MoveRightSyb);
            InputManager.GetInst.AddBindFunction("Left", InputType.Down, MoveLeftSyb);
            InputManager.GetInst.AddBindFunction("Z", InputType.Down, ZPush);
            InputManager.GetInst.AddBindFunction("X", InputType.Down, XPush);

            return true;
        }

        public override void Render() {
            if(((CharCreateScene)mOwner).CurStep != Step.InputName) {
                PrintText(Sym.sym, Sym.X, Sym.Y);
            }
        }

        public override void Update() {
            if (((CharCreateScene)mOwner).CurStep == Step.ClassSelect && Sym.Y != 12)
                SetSybPos(31, 12);

            if (((CharCreateScene)mOwner).CurStep == Step.End && Sym.Y != 21)
                SetSybPos(37, 21);
        }

        private void MoveDownSyb() {
            if (((CharCreateScene)mOwner).CurStep == Step.StatusSelect) {
                int y = Sym.Y == (int)Status.End - 1 ? Sym.Y : Sym.Y + 1;
                SetSybPos(Sym.X, y);

                if (((CharCreateScene)mOwner).CurStatus != Status.End - 1)
                    ((CharCreateScene)mOwner).CurStatus++;
            }
        }

        private void MoveUpSyb() {
            if (((CharCreateScene)mOwner).CurStep == Step.StatusSelect) {
                int y = Sym.Y == (int)Status.Atk ? Sym.Y : Sym.Y - 1;
                SetSybPos(Sym.X, y);

                if (((CharCreateScene)mOwner).CurStatus != Status.Atk)
                    ((CharCreateScene)mOwner).CurStatus--;
            }
        }

        private void MoveRightSyb() {
            if (((CharCreateScene)mOwner).CurStep == Step.ClassSelect &&
                ((CharCreateScene)mOwner).SelectClass != CharClass.Max - 1) {
                ((CharCreateScene)mOwner).SelectClass++;
                MoveSyb(10, 0);
            }

            if (((CharCreateScene)mOwner).CurStep == Step.End) {
                SetSybPos(47, 21);
            }
        }
        
        private void MoveLeftSyb() {
            if (((CharCreateScene)mOwner).CurStep == Step.ClassSelect &&
                ((CharCreateScene)mOwner).SelectClass != CharClass.Warrior) {
                ((CharCreateScene)mOwner).SelectClass--;
                MoveSyb(-10, 0);
            }

            if (((CharCreateScene)mOwner).CurStep == Step.End) {
                SetSybPos(37, 21);
            }
        }

        private void ZPush() {
            if (((CharCreateScene)mOwner).CurStep == Step.ClassSelect) {
                ((CharCreateScene)mOwner).CurStep++;
                SetSybPos(31, 16);
            }

            if (((CharCreateScene)mOwner).CurStep == Step.StatusSelect) {
                UpdateStatus();
                PrintText(statusSelect, 31, 15);
                if (((CharCreateScene)mOwner).BasePoint == 0) {
                    ((CharCreateScene)mOwner).CurStep++;
                }
            }
        }

        private void XPush() {
            if (((CharCreateScene)mOwner).CurStep == Step.StatusSelect) {
                UpdateStatus();
                PrintText(statusSelect, 31, 15);
            }
        }

        private void UpdateStatus() {
            CharCreateScene Scene = (CharCreateScene)mOwner;
            statusSelect = new List<string> {
                "3. 스탯 분배를 해주세요. (남은 포인트 : " + Scene.BasePoint.ToString("D2") + ")",
                "  공격력 : " + Scene.BaseAtk,
                "  방어력 : " + Scene.BaseDef,
                "  체력   : " + Scene.BaseHp
            };
        }
    }
}
