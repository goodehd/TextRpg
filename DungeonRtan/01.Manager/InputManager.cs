using System.Runtime.InteropServices;

enum InputType {
    Down,
    Push,
    Up,
    End
}

public class KeyState {
    public char key;
    public bool down;
    public bool push;
    public bool up;
}

public class BindKey {
    public KeyState key;
    public List<Action> actions;
}

namespace DungeonRtan.Manager {
    internal class InputManager : IGameComponent{
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        Dictionary<char, KeyState> mDicKeyState;
        Dictionary<string, BindKey> mDicBindKey;

        private InputManager() {
            mDicKeyState = new Dictionary<char, KeyState>();
            mDicBindKey = new Dictionary<string, BindKey>();
        }

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

        public void Render() {
            
        }

        public void Update() {
            UpdateKeyStat();
            UpdateBind();
        }

        private void UpdateKeyStat() {
            foreach(KeyState value in mDicKeyState.Values) {
                bool keyPush = false;

                int keyState = GetAsyncKeyState(value.key);
                if ((keyState & 0x8000) != 0) {
                    keyPush = true;
                }

                if (keyPush) {
                    if(!value.down && !value.push) {
                        value.down = true;
                        value.push = true;
                    } else {
                        value.down = false;
                    }
                } else if(value.push) {
                    value.up = true;
                    value.down = false;
                    value.push = false;
                } else if (value.up) {
                    value.up = false;
                }
            }
        }

        private void UpdateBind() {
            foreach (BindKey value in mDicBindKey.Values) {
                if (value.key.down && value.actions[(int)InputType.Down] != null) {
                    value.actions[(int)InputType.Down]();
                } else if (value.key.push && value.actions[(int)InputType.Push] != null) {
                    value.actions[(int)InputType.Push]();
                } else if (value.key.up && value.actions[(int)InputType.Up] != null) {
                    value.actions[(int)InputType.Up]();
                }
            }
        }

        public void AddBindKey(string name, char key) {
            if (mDicBindKey.ContainsKey(name))
                return;

            KeyState newState = new KeyState();
            newState.key = key;
            mDicKeyState.Add(key, newState);

            BindKey newKey = new BindKey();
            newKey.key = newState;
            newKey.actions = new List<Action> { null, null, null };
            mDicBindKey.Add(name, newKey);
        }

        public void AddBindFunction(string name, InputType type, Action action) {
            if (!mDicBindKey.ContainsKey(name))
                return;

            if (mDicBindKey[name].actions[(int)type] == null)
                mDicBindKey[name].actions[(int)type] = action;
            else
                mDicBindKey[name].actions[(int)type] += action;
        }

        public void ClearBind() {
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
