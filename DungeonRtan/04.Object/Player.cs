
using DungeonRtan.Object;

namespace DungeonRtan.Objects {
    public class Player : GameObject {
        public override string Name { get; set; }
        public override int Level { get; set; }
        public override int ATK { get; set; }
        public override int DEF { get; set; }
        public override int HP { get; set; }
        public override int Gold { get; set; }
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

        public override bool Init() {
            return true;
        }

        public override void Render() {

        }

        public override void Update() {

        }
    }
}
