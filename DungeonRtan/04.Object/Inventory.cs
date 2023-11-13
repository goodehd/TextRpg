
namespace DungeonRtan.Object {
    public class Inventory {
        public Item? CurWeapon { get; set; }
        public Item? CurArmor { get; set; }
        public List<Item> Items { get; set; }

        public Inventory() {
            Items = new List<Item>();
        }
    }
}
