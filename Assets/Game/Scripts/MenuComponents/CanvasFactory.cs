using Game.Scripts.AbilityComponents.ArcherAbilities;
using Game.Scripts.AbilityComponents.MeleeAbilities;
using Game.Scripts.PlayerComponents;
using Game.Scripts.PlayerComponents.Controller;
using System;
using UnityEngine;

namespace Game.Scripts.MenuComponents
{
    [CreateAssetMenu(fileName = "CanvasFactory", menuName = "CanvasFactory")]
    public class CanvasFactory : ScriptableObject
    {
        [SerializeField] private Canvas _meleeCanvas;
        [SerializeField] private Canvas _rangeCanvas;

        private bool _isJoystickActive;
        private Canvas _canvas;

        public Joystick MovementJoystick { get; private set; }
        public Joystick RotationJoystick { get; private set; }

        public Canvas Create(CharacterType characterType, Player player)
        {
            Canvas prefab = GetPrefab(characterType);

            if (prefab == null)
            {
                throw new ArgumentException("There was no Prefab of Canvas for the type of character: {characterType}");
            }

            Canvas canvas = Instantiate(prefab);
            PlayerDataViewer healthViewer = canvas.GetComponent<PlayerDataViewer>();

            healthViewer.Init(player);

            if (characterType == CharacterType.Melee)
            {
                MeleeWindowImprovement meleeWindow = canvas.GetComponent<MeleeWindowImprovement>();
                MeleeAbilityViewer meleeAbility = canvas.GetComponent<MeleeAbilityViewer>();
                MeleeCanvasInitialization meleeCanvasInit = canvas.GetComponent<MeleeCanvasInitialization>();

                meleeWindow.Init(player);
                meleeAbility.Init(player);
                meleeCanvasInit.InitButtons(player);

                _canvas = canvas;
            }
            else if (characterType == CharacterType.Range)
            {
                RangeWindowImprovement rangeWindow = canvas.GetComponent<RangeWindowImprovement>();
                RangeAbilityViewer rangeAbility = canvas.GetComponent<RangeAbilityViewer>();
                RangeCanvasInitialization rangeCanvasInit = canvas.GetComponent<RangeCanvasInitialization>();

                rangeWindow.Init(player);
                rangeAbility.Init(player);
                rangeCanvasInit.InitButtons(player);

                _canvas = canvas;
            }

            return canvas;
        }

        public void Init(bool isJoystickActive)
        {
            _isJoystickActive = isJoystickActive;

            if (_isJoystickActive)
            {
                MovementJoystick = _canvas.GetComponent<JoystickData>().MovementJoystick;
                RotationJoystick = _canvas.GetComponent<JoystickData>().RotationJoystick;
                MovementJoystick.gameObject.SetActive(true);
                RotationJoystick.gameObject.SetActive(true);
            }
        }

        private Canvas GetPrefab(CharacterType characterType)
        {
            switch (characterType)
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