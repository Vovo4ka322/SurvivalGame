using PlayerComponents;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeCharactersFactory", menuName = "GameplayExample/MeleeCharactersFactory")]
public class MeleePlayerFactory : ScriptableObject
{
    [SerializeField] private MeleePlayer _firstMeleeSkin;
    [SerializeField] private MeleePlayer _secondMeleeSkin;
    [SerializeField] private MeleePlayer _thirdMeleeSkin;

    public MeleePlayer Get(MeleeCharacterSkins skinType, Vector3 spawnPosition)
    {
        MeleePlayer instance = Instantiate(GetPrefab(skinType), spawnPosition, Quaternion.identity);

        return instance;
    }

    private MeleePlayer GetPrefab(MeleeCharacterSkins skinType)
    {
        switch (skinType)
        {
            case MeleeCharacterSkins.FirstMeleeSkin:
                return _firstMeleeSkin;
            case MeleeCharacterSkins.SecondMeleeSkin:
                return _secondMeleeSkin;
            case MeleeCharacterSkins.ThirdMeleeSkin:
                return _thirdMeleeSkin;
            
            default:
                throw new ArgumentException(nameof(skinType));
        }
    }
}
