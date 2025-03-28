using System;
using System.Linq;
using Game.Scripts.MenuComponents.ShopComponents.Data;
using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;

namespace Game.Scripts.MenuComponents.ShopComponents.Visitors
{
    public class OpenSkinsChecker : IShopItemVisitor
    {
        private IPersistentData _persistentData;

        public OpenSkinsChecker(IPersistentData persistentData) => _persistentData = persistentData;

        public event Action MeleeCharacterOpened;
        public event Action RangeCharacterOpened;

        public bool IsOpened { get; private set; }

        public void Visit(CharacterSkinItem characterSkinItem)
        {
            IsOpened = _persistentData.PlayerData.OpenCharacterSkins.Contains(characterSkinItem.SkinType);

            if (characterSkinItem.SkinType == CharacterSkins.FirstMeleeSkin)
                MeleeCharacterOpened?.Invoke();
            else if (characterSkinItem.SkinType == CharacterSkins.FirstRangeSkin)
                RangeCharacterOpened?.Invoke();
        }
    }
}