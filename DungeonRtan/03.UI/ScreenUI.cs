using DungeonRtan.Scenes;

namespace DungeonRtan.UI {
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
        public Scene mOwner { get; set; }
        public Symbol Sym { get; private set;}
        
        public virtual bool Init() {
            if(Sym == null)
                Sym = new Symbol("▶");
            return true;
        }
        public abstract void Render();
        public abstract void Update();

        protected void PrintText(List<string> txt) {
            for (int i = 0; i < txt.Count; i++) {
                Console.WriteLine(txt[i]);
            }
        }

        protected void PrintText(List<string> txt, int rightOffSet, int downOffSet) {
            for (int i = 0; i < txt.Count; i++) {
                Console.SetCursorPosition(rightOffSet, i + downOffSet);
                Console.WriteLine(txt[i]);
            }
        }

        protected void PrintText(string txt, int rightOffSet, int downOffSet) {
            Console.SetCursorPosition(rightOffSet, downOffSet);
            Console.WriteLine(txt);
        }

        protected void MoveSyb(int addX, int addY) {
            Console.SetCursorPosition(Sym.X, Sym.Y);
            Console.WriteLine("  ");
            Sym.X += addX;
            Sym.Y += addY;
        }

        protected void SetSybPos(int X, int Y) {
            Console.SetCursorPosition(Sym.X, Sym.Y);
            Console.WriteLine("  ");
            Sym.X = X;
            Sym.Y = Y;
        }

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
