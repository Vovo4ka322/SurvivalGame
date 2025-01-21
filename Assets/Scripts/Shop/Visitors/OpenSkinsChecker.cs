using System.Linq;

public class OpenSkinsChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsOpened { get; private set; }

    public OpenSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(MeleeCharacterSkinItem characterSkinItem) 
        => IsOpened = _persistentData.PlayerData.OpenMeleeCharacterSkins.Contains(characterSkinItem.SkinType);

    public void Visit(RangeCharacterSkinItem mazeSkinItem) 
        => IsOpened = _persistentData.PlayerData.OpenRangeCharacterSkins.Contains(mazeSkinItem.SkinType);
}
