using DungeonRtan.Object;
using DungeonRtan.Objects;
using DungeonRtan.UI;

namespace DungeonRtan.Scenes {
    internal class ShopScene : Scene {
        private List<Item> items;

        public override bool Init() {
            items = new List<Item>();
            items.Add(new Item("막대기", 5, 0, 100, 0, EItemType.Weapon, "마악대기이이이이", false, "공격력 : +5"));
            items.Add(new Item("돌맹이", 5, 0, 100, 0, EItemType.Weapon, "도올매앵이이이잉", false, "공격력 : +5"));
            items.Add(new Item("모자", 5, 0, 100, 0, EItemType.Armor, "모오오오오오오오옹자", false, "공격력 : +5"));
            items.Add(new Item("바지", 5, 0, 100, 0, EItemType.Armor, "바바아앙방바앙비지", false, "공격력 : +5"));

            items.Add(new Item("패딩", 5, 0, 100, 0, EItemType.Weapon, "패딩은 겨울 필수품이에요", false, "공격력 : +5"));
            items.Add(new Item("양말", 5, 0, 100, 0, EItemType.Weapon, "발이 너무 시려워요", false, "공격력 : +5"));
            items.Add(new Item("고양이", 5, 0, 1000, 0, EItemType.Armor, "나만 고양이 없는 것 같아요", false, "공격력 : +5"));
            items.Add(new Item("윤하의 벨트", 5, 0, 10000, 0, EItemType.Armor, "터져버린 흔적이 보여요", false, "공격력 : +5"));

            CreateUI<ItemsViewUI>().SetViewType(EItemViewType.Shop, items);
            return true;
        }

        public override void Render() {
            mSceneUI.Render();
        }

        public override void Update() {
            mSceneUI.Update();
        }

        public void BuyItem(int index, bool isBuy) {
            if (isBuy)
                return;

            if (mPlayer.Gold < items[index].Gold)
                return;

            mPlayer.Inven.AddItem(items[index]);
            mPlayer.Gold -= items[index].Gold;
            ((ItemsViewUI)mSceneUI).SetViewType(EItemViewType.Shop, items);
        }
    }
}
