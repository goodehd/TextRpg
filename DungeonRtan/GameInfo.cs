interface IGameComponent {
    bool Init();
    void Update();
    void Render();
}

interface IStatus {
    string Name { get; set; }
    int Level { get; set; }
    int ATK { get; set; }
    int DEF { get; set; }
    int HP { get; set; }
    int Gold { get; set; }
}

public struct Resolution {
    public int Width;
    public int Height;
}

public enum CharClass {
    Warrior,
    Wizard,
    Max
}

public enum EItemType {
    Weapon,
    Armor,
    Max
}