using Game.Scripts.MenuComponents.ShopComponents.Data;
using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;

namespace Game.Scripts.MenuComponents.ShopComponents.Visitors
{
    public class SkinUnlocker : IShopItemVisitor
    {
        private IPersistentData _persistentData;

        public SkinUnlocker(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(CharacterSkinItem characterSkinItem)
            => _persistentData.PlayerData.OpenCharacterSkin(characterSkinItem.SkinType);
    }
}