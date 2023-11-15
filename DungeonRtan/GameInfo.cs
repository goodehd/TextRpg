
// 게임 에서 사용하는 인터페이스와 enum을 모아놓은 곳

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

public enum EItemViewType {
    Equipped,
    View,
    ShopBuy,
    ShopSell,
    Max
}

public enum EDifficulty {
    Easy,
    Normal,
    Hard,
    Max
}

public enum EStartMenu {
    Start,
    Continue,
    Exit,
    Max
}