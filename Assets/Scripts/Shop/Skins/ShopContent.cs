using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<MeleeCharacterSkinItem> _meleeCharacterSkinItems;
    [SerializeField] private List<RangeCharacterSkinItem> _rangeCharacterSkinItems;

    //q
    [SerializeField] private List<CharacterSkinItem> _characterSkinItems;

    public IEnumerable<MeleeCharacterSkinItem> MeleeCharacterSkinItems => _meleeCharacterSkinItems;
    public IEnumerable<RangeCharacterSkinItem> RangeCharacterSkinItems => _rangeCharacterSkinItems;

    //q
    public IEnumerable<CharacterSkinItem> CharacterSkinItems => _characterSkinItems;

    private void OnValidate()
    {
        var meleeCharaterSkinsDuplicates = _meleeCharacterSkinItems.GroupBy(item => item.SkinType)
            .Where(array => array.Count() > 1);

        if (meleeCharaterSkinsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_meleeCharacterSkinItems));

        var rangeCharacterSkinsDuplicates = _rangeCharacterSkinItems.GroupBy(item => item.SkinType)
            .Where(array => array.Count() > 1);

        if (rangeCharacterSkinsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_rangeCharacterSkinItems));

        //q
        var characterCkinsDuplicates = _characterSkinItems.GroupBy(item => item.SkinType)
            .Where(array => array.Count() > 1);

        if(characterCkinsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_characterSkinItems));
    }
}
