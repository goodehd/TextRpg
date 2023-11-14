
using DungeonRtan.Object;
using System.Xml.Linq;

namespace DungeonRtan.Objects {
    public class Player : GameObject {
        public int MaxHP { get; private set; }
        public int ClearCount { get; set; }

        public CharClass Chad { get; set;}
        public Inventory Inven { get; set; }

        public Player(string name, int atk, int def, int hp) {
            Name = name;
            ATK = atk;
            DEF = def;
            HP = hp;
            MaxHP = hp;
            Level = 1;
            Gold = 1000;

            Inven = new Inventory();
        }

        public Player() { }

        public void Save() {
            string projectPath = Directory.GetCurrentDirectory();
            string[] pathSegments = projectPath.Split(new[] { "bin" }, StringSplitOptions.None);
            projectPath = pathSegments[0] + "Save\\player.bin";

            FileStream fs = File.Open(projectPath, FileMode.Create);

            using (BinaryWriter wr = new BinaryWriter(fs)) {
                wr.Write(Name);
                wr.Write(Level);
                wr.Write(ATK);
                wr.Write(DEF);
                wr.Write(HP);
                wr.Write(Gold);
                wr.Write(MaxHP);
                wr.Write(ClearCount);
                wr.Write((int)Chad);
                Inven.Save(wr);
            }
        }

        public void Load() {
            string projectPath = Directory.GetCurrentDirectory();
            string[] pathSegments = projectPath.Split(new[] { "bin" }, StringSplitOptions.None);
            projectPath = pathSegments[0] + "Save\\player.bin";

            FileStream fs = File.Open(projectPath, FileMode.Open);

            using (BinaryReader rdr = new BinaryReader(fs)) {
                Name = rdr.ReadString();
                Level = rdr.ReadInt32();
                ATK = rdr.ReadInt32();
                DEF = rdr.ReadInt32();
                HP = rdr.ReadInt32();
                Gold = rdr.ReadInt32();
                MaxHP = rdr.ReadInt32();
                ClearCount = rdr.ReadInt32();
                Chad = (CharClass)rdr.ReadInt32();

                Inven = new Inventory();
                Inven.Load(rdr);
            }
        }
    }
}
