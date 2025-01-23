using PlayerComponents;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactersFactory", menuName = "GameplayExample/CharactersFactory")]
public class GeneralPlayerFactory : ScriptableObject
{
    [SerializeField] private Player _firstMeleeSkin;
    [SerializeField] private Player _secondMeleeSkin;
    [SerializeField] private Player _thirdMeleeSkin;
    [SerializeField] private Player _firstRangeSkin;
    [SerializeField] private Player _secondRangeSkin;
    [SerializeField] private Player _thirdRangeSkin;

    public Player Get(CharacterSkins characterSkins, Vector3 spawnPosition)
    {
        Player player = Instantiate(GetPrefab(characterSkins), spawnPosition, Quaternion.identity);

        return player;
    }

    private Player GetPrefab(CharacterSkins skinType)
    {
        switch (skinType)
        {
            case CharacterSkins.FirstMeleeSkin:
                return _firstMeleeSkin;

            case CharacterSkins.SecondMeleeSkin:
                return _secondMeleeSkin;

            case CharacterSkins.ThirdMeleeSkin:
                return _thirdMeleeSkin;

            case CharacterSkins.FirstRangeSkin:
                return _firstRangeSkin;

            case CharacterSkins.SecondRangeSkin:
                return _secondRangeSkin;

            case CharacterSkins.ThirdRangeSkin:
                return _thirdRangeSkin;

            default:
                throw new ArgumentException(nameof(skinType));
        }
    }
}