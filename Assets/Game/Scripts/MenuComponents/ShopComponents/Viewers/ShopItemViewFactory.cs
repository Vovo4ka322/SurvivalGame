using UnityEngine;
using MenuComponents.ShopComponents.SkinComponents;
using MenuComponents.ShopComponents.Visitors;

namespace MenuComponents.ShopComponents.Viewers
{
    [CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
    public class ShopItemViewFactory : ScriptableObject
    {
        [SerializeField] private ShopItemView _characterSkinItemPrefab;

        public ShopItemView Get(ShopItem shopItem, Transform parent)
        {
            ShopItemVisitor visitor = new ShopItemVisitor(_characterSkinItemPrefab);
            visitor.Visit(shopItem);

            ShopItemView instance = Instantiate(visitor.Prefab, parent);
            instance.Initialize(shopItem);

            return instance;
        }

        private class ShopItemVisitor : IShopItemVisitor
        {
            ShopItemView _characterSkinItemPrefab;

            public ShopItemVisitor(ShopItemView generalCharacterSkinItemPrefab)
            {
                _characterSkinItemPrefab = generalCharacterSkinItemPrefab;
            }

            public ShopItemView Prefab { get; private set; }

            public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

            public void Visit(CharacterSkinItem characterSkinItem) => Prefab = _characterSkinItemPrefab;
        }
    }
}