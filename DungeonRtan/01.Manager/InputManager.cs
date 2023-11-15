using System.Runtime.InteropServices;

// 키 입력 상태를 나타내는 enum
enum InputType {
    Down,
    Push,
    Up,
    End
}

// 어느 키를 사용할지와 키 상태를 담고있는 클래스
public class KeyState {
    public char key;
    public bool down;
    public bool push;
    public bool up;
}

// 등록된 키를 눌렀을 때 키 상태에 따라 함수를 등록해주는 클래스.
public class BindKey {
    public KeyState key;
    public List<Action> actions;
}

namespace DungeonRtan.Manager {
    internal class InputManager : IGameComponent{
        // 키 입력처리를 위한 라이브러리 임포트
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        //등록된 키와 키 상태에 따라 호출될 함수를 관리할 자료구조
        Dictionary<char, KeyState> mDicKeyState;
        Dictionary<string, BindKey> mDicBindKey;

        private InputManager() {
            mDicKeyState = new Dictionary<char, KeyState>();
            mDicBindKey = new Dictionary<string, BindKey>();
        }

        // 어느 키를 사용할지 등록하는 함수
        public bool Init() {
            // 키 코드 찾기: https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
            AddBindKey("Up", (char)0x26);
            AddBindKey("Down", (char)0x28);
            AddBindKey("Left", (char)0x25);
            AddBindKey("Right", (char)0x27);

            AddBindKey("Z", 'Z');
            AddBindKey("X", 'X');

            return true;
        }

        public void Render() { } 

        public void Update() {
            // 등록된 키 상태를 확인하고, 
            UpdateKeyStat();

            // 키 상태에 따라 등록된 함수를 실행한다.
            UpdateBind();
        }

        private void UpdateKeyStat() {
            foreach(KeyState value in mDicKeyState.Values) {
                bool keyPush = false;

                //키가 눌렸는지 확인한다.
                int keyState = GetAsyncKeyState(value.key);
                if ((keyState & 0x8000) != 0) {
                    keyPush = true;
                }

                // 키가 눌렸다면
                if (keyPush) {
                    //키가 눌렸는데 down과 push가 false일 경우 이 키는 처음 눌린 상태
                    if(!value.down && !value.push) {
                        value.down = true;
                        value.push = true;
                    } else {
                        // 둘중 하나라도 true인 경우는 키가 눌려지고 있는 상태
                        value.down = false;
                    }
                } else if(value.push) {
                    //키가 눌리지 않았는데 push가 true일 경우 키가 누르고 있는 것을 놓은 경우
                    value.up = true;
                    value.down = false;
                    value.push = false;
                } else if (value.up) {
                    // 키가 안눌려진 상태로 전환.
                    value.up = false;
                }
            }
        }

        private void UpdateBind() {
            // 등록된 키에 연결된 함수 정보가 있는 자료구조를 순회한다.
            foreach (BindKey value in mDicBindKey.Values) {
                
                // down상태이고 등록된 함수가 있다면 함수를 실행한다.
                if (value.key.down && value.actions[(int)InputType.Down] != null) {
                    value.actions[(int)InputType.Down]();
                }

                // push상태이고 등록된 함수가 있다면 함수를 실행한다.
                else if (value.key.push && value.actions[(int)InputType.Push] != null) {
                    value.actions[(int)InputType.Push]();
                }

                // up상태이고 등록된 함수가 있다면 함수를 실행한다.
                else if (value.key.up && value.actions[(int)InputType.Up] != null) {
                    value.actions[(int)InputType.Up]();
                }
            }
        }

        public void AddBindKey(string name, char key) {
            // 이미 등록된 이름일 경우 예외 처리
            if (mDicBindKey.ContainsKey(name))
                return;

            // 등록된 키를 바탕으로 키 스테이트를 만듦
            KeyState newState = new KeyState();
            newState.key = key;
            mDicKeyState.Add(key, newState);

            BindKey newKey = new BindKey();
            newKey.key = newState;
            newKey.actions = new List<Action> { null, null, null }; // 기본적으로 아무 함수도 등록되지 않게 처리
            mDicBindKey.Add(name, newKey);
        }

        public void AddBindFunction(string name, InputType type, Action action) {
            // 등록 되지 않은 키일경우 예외 처리
            if (!mDicBindKey.ContainsKey(name))
                return;

            //처음 등록된 함수일 경우 해당 함수를 넣어주고,
            //두번쨰로 등록된 함수일 경우 뒤에 이어 붙여준다
            if (mDicBindKey[name].actions[(int)type] == null)
                mDicBindKey[name].actions[(int)type] = action;
            else
                mDicBindKey[name].actions[(int)type] += action;
        }

        public void ClearBind() {
            // scene마다 키에 등록된 함수가 달라야 하기 때문에 연결된 함수를 모두 지워주는 함수
            foreach (BindKey value in mDicBindKey.Values) {
                for(int i =0; i < (int)InputType.End; ++i) {
                    value.actions[i] = null;
                }
            }
        }

        private static InputManager instance;

        public static InputManager GetInst {
            get {
                if (instance == null) {
                    instance = new InputManager();
                }
                return instance;
            }
        }
    }
}
