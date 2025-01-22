using System.Linq;

public class OpenSkinsChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsOpened { get; private set; }

    public OpenSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(MeleeCharacterSkinItem meleeCharacterSkinItem) 
        => IsOpened = _persistentData.PlayerData.OpenMeleeCharacterSkins.Contains(meleeCharacterSkinItem.SkinType);

    public void Visit(RangeCharacterSkinItem rangeCharacterSkinItem) 
        => IsOpened = _persistentData.PlayerData.OpenRangeCharacterSkins.Contains(rangeCharacterSkinItem.SkinType);
}
