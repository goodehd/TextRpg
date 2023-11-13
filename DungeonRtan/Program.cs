using DungeonRtan.Manager;

namespace DungeonRtan {
    internal class Program {
        static int Main(string[] args) {
            if (!GameManager.GetInst.Init())
                return 0;

            GameManager.GetInst.Run();

            return 0;
        }
    }
}