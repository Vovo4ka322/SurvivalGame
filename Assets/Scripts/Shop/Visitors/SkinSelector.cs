public class SkinSelector : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public SkinSelector(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(MeleeCharacterSkinItem characterSkinItem) 
        => _persistentData.PlayerData.SelectedMeleeCharacterSkin = characterSkinItem.SkinType;

    public void Visit(RangeCharacterSkinItem mazeSkinItem) 
        => _persistentData.PlayerData.SelectedRangeCharacterSkin = mazeSkinItem.SkinType;

    //q
    public void Visit(CharacterSkinItem characterSkinItem)
        => _persistentData.PlayerData.SelectedCharacterSkin = characterSkinItem.SkinType;
}
