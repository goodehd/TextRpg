using DungeonRtan.Scenes;

namespace DungeonRtan.UI {

    //Scene에서 사용하는 "▶"를 표현하는 클래스
    public class Symbol {
        public int X { get; set; }
        public int Y { get; set; }
        public string sym;

        public Symbol(string sym) {
            this.sym = sym;
            X = 0;
            Y = 0;
        }
    }

    public abstract class ScreenUI : IGameComponent {
        public Scene mOwner { get; set; }        // 어느 scene에 속해있는지
        public Symbol Sym { get; private set; }  // 위에 있는 Symbol

        protected int baseX = 0;
        protected int baseY = 0;

        public virtual bool Init() {
            if(Sym == null)
                Sym = new Symbol("▶");
            return true;
        }
        public abstract void Render();
        public abstract void Update();

        // 글자를 0, 0에 출력한다.
        protected void PrintText(List<string> txt) {
            for (int i = 0; i < txt.Count; i++) {
                Console.WriteLine(txt[i]);
            }
        }

        // 글자를 downOffSet, rightOffSet에 출력한다.
        protected void PrintText(List<string> txt, int rightOffSet, int downOffSet) {
            for (int i = 0; i < txt.Count; i++) {
                Console.SetCursorPosition(rightOffSet, i + downOffSet);
                Console.WriteLine(txt[i]);
            }
        }

        // List<string>이 아닌 string 글자를 downOffSet, rightOffSet에 출력한다.
        protected void PrintText(string txt, int rightOffSet, int downOffSet) {
            Console.SetCursorPosition(rightOffSet, downOffSet);
            Console.WriteLine(txt);
        }

        // "▶" 의 기존 위치를 기반으로 움직이는 함수
        protected void MoveSyb(int addX, int addY) {
            Console.SetCursorPosition(Sym.X, Sym.Y);
            Console.WriteLine("  ");
            Sym.X += addX;
            Sym.Y += addY;
        }

        // "▶" 의 위치를 고정 위치로 이동 시키는 함수
        protected void SetSybPos(int X, int Y) {
            Console.SetCursorPosition(Sym.X, Sym.Y);
            Console.WriteLine("  ");
            Sym.X = X;
            Sym.Y = Y;
        }

        // 중앙 정렬로 출력하는 함수인데 안됨, 사용x
        protected void CenterTest(List<string> txt, int downOffSet) {
            for (int i = 0; i < txt.Count; i++) {
                float half = Console.WindowWidth / 2.0f;
                half -= txt[i].Length;
                Console.SetCursorPosition((int)half, i + downOffSet);
                Console.WriteLine(txt[i]);
            }
        }
    }
}
