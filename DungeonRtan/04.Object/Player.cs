
using DungeonRtan.Object;

namespace DungeonRtan.Objects {
    public class Player : GameObject {

        public CharClass Chad { get; set;}
        public Inventory Inven { get; set; }

        public Player(string name, int atk, int def, int hp) {
            Name = name;
            ATK = atk;
            DEF = def;
            HP = hp;
            Level = 1;
            Gold = 1000;

            Inven = new Inventory();
        }
    }
}
