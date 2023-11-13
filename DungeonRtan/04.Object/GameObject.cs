
namespace DungeonRtan.Objects {
    public abstract class GameObject : IStatus, IGameComponent {
        public abstract string Name { get; set; }
        public abstract int Level { get; set; }
        public abstract int ATK { get; set; }
        public abstract int DEF { get; set; }
        public abstract int HP { get; set; }
        public abstract int Gold { get; set; }

        public abstract bool Init();
        public abstract void Render();
        public abstract void Update();
    }
}
