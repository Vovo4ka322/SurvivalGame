public class SkinUnlocker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public SkinUnlocker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(MeleeCharacterSkinItem characterSkinItem) 
        => _persistentData.PlayerData.OpenMeleeCharacterSkin(characterSkinItem.SkinType);

    public void Visit(RangeCharacterSkinItem mazeSkinItem) 
        => _persistentData.PlayerData.OpenRangeCharacterSkin(mazeSkinItem.SkinType);

    //q
    public void Visit(CharacterSkinItem characterSkinItem)
        => _persistentData.PlayerData.OpenCharacterSkin(characterSkinItem.SkinType);
}
