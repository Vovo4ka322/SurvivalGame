using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _meleeCharacterSkinItemPrefab;
    [SerializeField] private ShopItemView _rangeCharacterSkinItemPrefab;

    //q
    [SerializeField] private ShopItemView _characterSkinItemPrefab;

    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemVisitor visitor = new ShopItemVisitor(_meleeCharacterSkinItemPrefab, _rangeCharacterSkinItemPrefab, _characterSkinItemPrefab);
        visitor.Visit(shopItem);

        ShopItemView instance = Instantiate(visitor.Prefab, parent);
        instance.Initialize(shopItem);

        return instance;
    }

    private class ShopItemVisitor : IShopItemVisitor
    {
        private ShopItemView _meleeCharacterSkinItemPrefab;
        private ShopItemView _rangeCharacterSkinItemPrefab;

        //q
        ShopItemView _characterSkinItemPrefab;

        public ShopItemVisitor(ShopItemView characterSkinItemPrefab, ShopItemView mazeSkinItemPrefab, ShopItemView generalCharacterSkinItemPrefab)
        {
            _meleeCharacterSkinItemPrefab = characterSkinItemPrefab;
            _rangeCharacterSkinItemPrefab = mazeSkinItemPrefab;
            _characterSkinItemPrefab = generalCharacterSkinItemPrefab;
        }

        public ShopItemView Prefab { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(MeleeCharacterSkinItem meleeCharacterSkinItem) => Prefab = _meleeCharacterSkinItemPrefab;

        public void Visit(RangeCharacterSkinItem rangeCharacterSkinItem) => Prefab = _rangeCharacterSkinItemPrefab;


        //q
        public void Visit(CharacterSkinItem characterSkinItem) => Prefab = _characterSkinItemPrefab;
    }
}
