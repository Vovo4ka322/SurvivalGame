using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;
using Game.Scripts.PlayerComponents;
using System;
using UnityEngine;

namespace Game.Scripts.MenuComponents.ShopComponents.GameplaySceneTest
{
    [CreateAssetMenu(fileName = "CharactersFactory", menuName = "GameplayExample/CharactersFactory")]
    public class GeneralPlayerFactory : ScriptableObject
    {
        [SerializeField] private Player _firstMeleeSkin;
        [SerializeField] private Player _firstRangeSkin;

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

                case CharacterSkins.FirstRangeSkin:
                    return _firstRangeSkin;

                default:
                    throw new ArgumentException(nameof(skinType));
            }
        }
    }
}