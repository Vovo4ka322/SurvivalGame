using Game.Scripts.MenuComponents.ShopComponents.Data;
using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;

namespace Game.Scripts.MenuComponents.ShopComponents.Visitors
{
    public class SkinUnlocker : IShopItemVisitor
    {
        private readonly IPersistentData _persistentData;

        public SkinUnlocker(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(CharacterSkinItem characterSkinItem) => _persistentData.PlayerData.OpenCharacterSkin(characterSkinItem.SkinType);
    }
}