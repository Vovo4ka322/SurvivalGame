public interface IShopItemVisitor
{
    void Visit(ShopItem shopItem);
    void Visit(MeleeCharacterSkinItem meleeCharacterSkinItem);
    void Visit(RangeCharacterSkinItem rangeCharacterSkinItem);

    //q
    public void Visit(CharacterSkinItem characterSkinItem);
}
