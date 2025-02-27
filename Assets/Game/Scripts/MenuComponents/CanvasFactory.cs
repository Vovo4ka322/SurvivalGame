using System;
using UnityEngine;
using Game.Scripts.PlayerComponents;

namespace Game.Scripts.MenuComponents
{
    [CreateAssetMenu(fileName = "CanvasFactory", menuName = "CanvasFactory")]
    public class CanvasFactory : ScriptableObject
    {
        [SerializeField] private Canvas _meleeCanvas;
        [SerializeField] private Canvas _rangeCanvas;

        public Canvas Create(CharacterType characterType, Player player)
        {
            Canvas prefab = GetPrefab(characterType);

            if(prefab == null)
            {
                throw new ArgumentException("There was no Prefab of Canvas for the type of character: {characterType}");
            }

            Canvas canvas = Instantiate(prefab);
            PlayerHealthAndExperienceViewer healthViewer = canvas.GetComponent<PlayerHealthAndExperienceViewer>();

            healthViewer.Init(player);

            if(characterType == CharacterType.Melee)
            {
                MeleeWindowImprovment meleeWindow = canvas.GetComponent<MeleeWindowImprovment>();
                MeleeAbilityViewer meleeAbility = canvas.GetComponent<MeleeAbilityViewer>();
                MeleeCanvasInitialization meleeCanvasInit = canvas.GetComponent<MeleeCanvasInitialization>();

                meleeWindow.Init(player);
                meleeAbility.Init(player);
                meleeCanvasInit.InitButtons(player);
            }
            else if(characterType == CharacterType.Range)
            {
                RangeWindowImprovment rangeWindow = canvas.GetComponent<RangeWindowImprovment>();
                RangeAbilityViewer rangeAbility = canvas.GetComponent<RangeAbilityViewer>();
                RangeCanvasInitialization rangeCanvasInit = canvas.GetComponent<RangeCanvasInitialization>();

                rangeWindow.Init(player);
                rangeAbility.Init(player);
                rangeCanvasInit.InitButtons(player);
            }

            return canvas;
        }

        private Canvas GetPrefab(CharacterType characterType)
        {
            switch(characterType)
            {
                case CharacterType.Melee:
                    return _meleeCanvas;
                case CharacterType.Range:
                    return _rangeCanvas;

                default:
                    throw new ArgumentException($"The wrong meaning {nameof(characterType)}: {characterType}");
            }
        }
    }
}