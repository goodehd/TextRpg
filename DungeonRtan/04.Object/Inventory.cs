
using System;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace DungeonRtan.Object {
    public class Inventory {
        public Item? CurWeapon { get; set; }
        public Item? CurArmor { get; set; }
        public List<Item> Items { get; set; }

        public Inventory() {
            Items = new List<Item>();

            ////테스트 코드
            //CurWeapon = new Item("낡은 검", 5, 0, 100, 0, EItemType.Weapon, "낡은 거어어어엄", true, "공격력 : +5");
            //CurArmor = new Item("무쇠 갑옷", 0, 2, 100, 0, EItemType.Armor, "무쇠에에에에 갑오옷", true, "방어력 +2");

            //Items.Add(CurWeapon);
            //Items.Add(CurArmor);
            //Items.Add(new Item("막대기", 5, 0, 100, 0, EItemType.Weapon, "마악대기이이이이", false, "공격력 : +5"));
            //Items.Add(new Item("돌맹이", 5, 0, 100, 0, EItemType.Weapon, "도올매앵이이이잉", false, "공격력 : +5"));
            //Items.Add(new Item("모자", 5, 0, 100, 0, EItemType.Armor, "모오오오오오오오옹자", false, "공격력 : +5"));
            //Items.Add(new Item("바지", 5, 0, 100, 0, EItemType.Armor, "바바아앙방바앙비지", false, "공격력 : +5"));
        }

        public void Init() {
            CurWeapon = new Item("낡은 검", 5, 0, 100, 0, EItemType.Weapon, "낡은 거어어어엄", true, "공격력 : +5");
            CurArmor = new Item("무쇠 갑옷", 0, 2, 100, 0, EItemType.Armor, "무쇠에에에에 갑오옷", true, "방어력 +2");
            Items.Add(CurWeapon);
            Items.Add(CurArmor);
        }

        public void ChangeCurItem(Item item) {
            if(item.Type == EItemType.Weapon) {

                if(CurWeapon != null)
                    CurWeapon.isEquipped = false;

                CurWeapon = item;
                item.isEquipped = true;
            }

            if (item.Type == EItemType.Armor) {

                if (CurArmor != null)
                    CurArmor.isEquipped = false;

                CurArmor = item;
                item.isEquipped = true;
            }
        }

        public void Unequip(EItemType type) {
            switch (type) {
                case EItemType.Weapon:
                    CurWeapon.isEquipped = false;
                    CurWeapon = null;
                    break;
                case EItemType.Armor:
                    CurArmor.isEquipped = false;
                    CurArmor = null;
                    break;
                case EItemType.Max:
                    break;
                default:
                    break;
            }
        }

        public void AddItem(Item item) {
            Items.Add(item);
        }

        public void Save(BinaryWriter wr) {
            wr.Write(Items.Count);
            for(int i = 0; i < Items.Count; i++) {
                Items[i].Save(wr);
            }

            bool isCurWeapon = false;

            if (CurWeapon != null)
                isCurWeapon = true;

            wr.Write(isCurWeapon);

            if (isCurWeapon)
                wr.Write(CurWeapon.Name);

            bool isArmor = false;

            if (CurArmor != null)
                isArmor = true;

            wr.Write(isArmor);

            if (isArmor)
                wr.Write(CurArmor.Name);
        }

        public void Load(BinaryReader rdr) {
            int itemsCount = rdr.ReadInt32();
            for (int i = 0; i < itemsCount; ++i) {
                string name = rdr.ReadString();
                int atk = rdr.ReadInt32();
                int def = rdr.ReadInt32();
                int gold = rdr.ReadInt32();
                int hp = rdr.ReadInt32();
                int type = rdr.ReadInt32();
                string desc = rdr.ReadString();
                bool isequip = rdr.ReadBoolean();
                string abildesc = rdr.ReadString();

                Items.Add(new Item(name, atk, def, gold, hp, (EItemType)type, desc, isequip, abildesc));
            }

            bool isWepone = rdr.ReadBoolean();
            if(isWepone) {
                string weponeName = rdr.ReadString();
                CurWeapon = Items.Find(x => x.Name == weponeName);
                CurWeapon.isEquipped = true;
            }

            bool isArmor = rdr.ReadBoolean();
            if (isArmor) {
                string ArmorName = rdr.ReadString();
                CurArmor = Items.Find(x => x.Name == ArmorName);
                CurArmor.isEquipped = true;
            }
        }
    }
}
