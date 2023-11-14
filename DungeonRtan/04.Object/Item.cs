using DungeonRtan.Objects;

namespace DungeonRtan.Object {
    public class Item : GameObject {
        public EItemType Type { get; set; }
        public bool isEquipped { get; set; }
        public string Descrip { get; set;}
        public string AbilDescrip { get; set; }

        public Item(string name, int atk, int def, int gold, int hp, 
            EItemType type, string descrip, bool isEquipped, string AbilDesc) {
            Name = name;
            ATK = atk;
            DEF = def;
            Gold = gold;
            HP = hp;
            Type = type;
            Descrip = descrip;
            this.isEquipped = isEquipped;
            AbilDescrip = AbilDesc;
        }
    }
}
