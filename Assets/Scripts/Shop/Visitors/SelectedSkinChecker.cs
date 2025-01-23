public class SelectedSkinChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsSelected { get; private set; }

    public SelectedSkinChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(MeleeCharacterSkinItem characterSkinItem) 
        => IsSelected = _persistentData.PlayerData.SelectedMeleeCharacterSkin == characterSkinItem.SkinType;

    public void Visit(RangeCharacterSkinItem mazeSkinItem) 
        => IsSelected = _persistentData.PlayerData.SelectedRangeCharacterSkin == mazeSkinItem.SkinType;

    //q
    public void Visit(CharacterSkinItem characterSkinItem)
        => IsSelected = _persistentData.PlayerData.SelectedCharacterSkin == characterSkinItem.SkinType;
}
