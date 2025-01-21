using UnityEngine;

[CreateAssetMenu(fileName = "MeleeCharacterSkinItem", menuName = "Shop/MeleeCharacterSkinItem")]
public class MeleeCharacterSkinItem : ShopItem
{
    [field: SerializeField] public MeleeCharacterSkins SkinType { get; private set; }
}
