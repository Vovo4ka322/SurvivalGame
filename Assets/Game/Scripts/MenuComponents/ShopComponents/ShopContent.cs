using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.MenuComponents.ShopComponents
{
    [CreateAssetMenu(fileName = "ShopContent", menuName = "Shop/ShopContent")]
    public class ShopContent : ScriptableObject
    {
        [SerializeField] private List<CharacterSkinItem> _characterSkinItems;

        public IEnumerable<CharacterSkinItem> CharacterSkinItems => _characterSkinItems;

        private void OnValidate()
        {
            var characterCkinsDuplicates = _characterSkinItems.GroupBy(item => item.SkinType).Where(array => array.Count() > 1);

            if (characterCkinsDuplicates.Count() > 0)
            {
                throw new InvalidOperationException(nameof(_characterSkinItems));
            }
        }
    }
}