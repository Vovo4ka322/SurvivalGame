using PlayerComponents;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeCharacterFactory", menuName = "GameplayExample/RangeCharacterFactory")]
public class RangePlayerFactory : ScriptableObject
{
    [SerializeField] private RangePlayer _firstRangeSkin;
    [SerializeField] private RangePlayer _secondRangeSkin;
    [SerializeField] private RangePlayer _thirdRangeSkin;


    public RangePlayer Get(RangeCharacterSkins skinType, Vector3 spawnPosition)
    {
        RangePlayer instance = Instantiate(GetPrefab(skinType), spawnPosition, Quaternion.identity, null);

        return instance;
    }

    public RangePlayer GetPrefab(RangeCharacterSkins skinType)
    {
        switch (skinType)
        {
            case RangeCharacterSkins.FirstRangeSkin:
                return _firstRangeSkin;
            case RangeCharacterSkins.SecondRangeSkin:
                return _secondRangeSkin;
            case RangeCharacterSkins.ThirdRangeShin:
                return _thirdRangeSkin;

            default:
                throw new ArgumentException(nameof(skinType));
        }
    }
}