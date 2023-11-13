using DungeonRtan.Objects;

namespace DungeonRtan.Object {
    public class Item : GameObject {
        public override string Name { get; set; }
        public override int Level { get; set; }
        public override int ATK { get; set; }
        public override int DEF { get; set; }
        public override int HP { get; set; }
        public override int Gold { get; set; }

        public EItemType Type { get; set; }
        public bool Used { get; set; }
        public string Explan {get; set;}

        public override bool Init() {
            return true;
        }

        public override void Render() {

        }

        public override void Update() {

        }
    }
}
