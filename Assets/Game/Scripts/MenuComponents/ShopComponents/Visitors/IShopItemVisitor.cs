using MenuComponents.ShopComponents.SkinComponents;

namespace MenuComponents.ShopComponents.Visitors
{
    public interface IShopItemVisitor
    {
        public void Visit(ShopItem shopItem);

        public void Visit(CharacterSkinItem characterSkinItem);
    }
}