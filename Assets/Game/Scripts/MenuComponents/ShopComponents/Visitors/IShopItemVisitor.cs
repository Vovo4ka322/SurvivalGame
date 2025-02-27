using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;

namespace Game.Scripts.MenuComponents.ShopComponents.Visitors
{
    public interface IShopItemVisitor
    {
        public void Visit(ShopItem shopItem);

        public void Visit(CharacterSkinItem characterSkinItem);
    }
}