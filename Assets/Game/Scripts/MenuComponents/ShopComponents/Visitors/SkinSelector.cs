using Game.Scripts.MenuComponents.ShopComponents.Data;
using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;

namespace Game.Scripts.MenuComponents.ShopComponents.Visitors
{
    public class SkinSelector : IShopItemVisitor
    {
        private IPersistentData _persistentData;

        public SkinSelector(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(CharacterSkinItem characterSkinItem) => _persistentData.PlayerData.SelectedCharacterSkin = characterSkinItem.SkinType;
    }
}