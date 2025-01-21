using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _meleeCharacterSkinItemPrefab;
    [SerializeField] private ShopItemView _rangeCharacterSkinItemPrefab;

    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemVisitor visitor = new ShopItemVisitor(_meleeCharacterSkinItemPrefab, _rangeCharacterSkinItemPrefab);
        visitor.Visit(shopItem);

        ShopItemView instance = Instantiate(visitor.Prefab, parent);
        instance.Initialize(shopItem);

        return instance;
    }

    private class ShopItemVisitor : IShopItemVisitor
    {
        private ShopItemView _meleeCharacterSkinItemPrefab;
        private ShopItemView _rangeCharacterSkinItemPrefab;

        public ShopItemVisitor(ShopItemView characterSkinItemPrefab, ShopItemView mazeSkinItemPrefab)
        {
            _meleeCharacterSkinItemPrefab = characterSkinItemPrefab;
            _rangeCharacterSkinItemPrefab = mazeSkinItemPrefab;
        }

        public ShopItemView Prefab { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(MeleeCharacterSkinItem meleeCharacterSkinItem) => Prefab = _meleeCharacterSkinItemPrefab;

        public void Visit(RangeCharacterSkinItem rangeCharacterSkinItem) => Prefab = _rangeCharacterSkinItemPrefab;
    }
}
