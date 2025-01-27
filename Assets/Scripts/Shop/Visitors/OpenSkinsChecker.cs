using System;
using System.Linq;

public class OpenSkinsChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public event Action MeleeCharacterOpened;
    public event Action RangeCharacterOpened;

    public bool IsOpened { get; private set; }

    public OpenSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(CharacterSkinItem characterSkinItem)
    {
        IsOpened = _persistentData.PlayerData.OpenCharacterSkins.Contains(characterSkinItem.SkinType);

        if (characterSkinItem.SkinType == CharacterSkins.FirstMeleeSkin)
            MeleeCharacterOpened?.Invoke();
        else if (characterSkinItem.SkinType == CharacterSkins.FirstRangeSkin)
            RangeCharacterOpened?.Invoke();
    }
}
