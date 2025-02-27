using Game.Scripts.MenuComponents.ShopComponents.Data;
using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;

namespace Game.Scripts.MenuComponents.ShopComponents.Visitors
{
    public class SelectedSkinChecker : IShopItemVisitor
    {
        private IPersistentData _persistentData;

        public bool IsSelected { get; private set; }

        public SelectedSkinChecker(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(CharacterSkinItem characterSkinItem)
        {
            IsSelected = _persistentData.PlayerData.SelectedCharacterSkin == characterSkinItem.SkinType;
        }
    }
}