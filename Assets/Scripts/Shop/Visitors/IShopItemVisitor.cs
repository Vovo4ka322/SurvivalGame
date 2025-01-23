public interface IShopItemVisitor
{
    void Visit(ShopItem shopItem);

    public void Visit(CharacterSkinItem characterSkinItem);
}
