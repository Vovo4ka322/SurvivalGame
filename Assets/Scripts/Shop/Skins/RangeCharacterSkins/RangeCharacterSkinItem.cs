using UnityEngine;

[CreateAssetMenu(fileName = "RangeCharacterSkinItem", menuName = "Shop/RangeCharacterSkinItem")]
public class RangeCharacterSkinItem : ShopItem
{
    [field: SerializeField] public RangeCharacterSkins SkinType { get; private set; }
}
