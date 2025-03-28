using UnityEngine;
using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;
using Game.Scripts.MenuComponents.ShopComponents.Visitors;

namespace Game.Scripts.MenuComponents.ShopComponents.Viewers
{
    [CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
    public class ShopItemViewFactory : ScriptableObject
    {
        [SerializeField] private ShopItemView _characterSkinItemPrefab;

        public ShopItemView Get(CharacterSkinItem shopItem, Transform parent)
        {
            ShopItemVisitor visitor = new ShopItemVisitor(_characterSkinItemPrefab);
            visitor.Visit(shopItem);

            ShopItemView instance = Instantiate(visitor.Prefab, parent);
            instance.Initialize(shopItem);

            return instance;
        }

        private class ShopItemVisitor : IShopItemVisitor
        {
            private ShopItemView _characterSkinItemPrefab;

            public ShopItemVisitor(ShopItemView generalCharacterSkinItemPrefab)
            {
                _characterSkinItemPrefab = generalCharacterSkinItemPrefab;
            }

            public ShopItemView Prefab { get; private set; }

            public void Visit(CharacterSkinItem characterSkinItem)
            {
                Prefab = _characterSkinItemPrefab;
            }
        }
    }
}