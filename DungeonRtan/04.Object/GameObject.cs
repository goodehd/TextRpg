
namespace DungeonRtan.Objects {
    public abstract class GameObject : IStatus {
        public string Name { get; set; }
        public int Level { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }
    }
}
