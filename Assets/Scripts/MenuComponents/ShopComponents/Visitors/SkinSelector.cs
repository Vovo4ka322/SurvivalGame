using MenuComponents.ShopComponents.Data;
using MenuComponents.ShopComponents.SkinComponents;

namespace MenuComponents.ShopComponents.Visitors
{
    public class SkinSelector : IShopItemVisitor
    {
        private IPersistentData _persistentData;

        public SkinSelector(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

        public void Visit(CharacterSkinItem characterSkinItem)
            => _persistentData.PlayerData.SelectedCharacterSkin = characterSkinItem.SkinType;
    }
}