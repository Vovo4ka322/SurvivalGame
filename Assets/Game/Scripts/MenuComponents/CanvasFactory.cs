using System;
using UnityEngine;
using Game.Scripts.AbilityComponents;
using Game.Scripts.PlayerComponents;
using Game.Scripts.PlayerComponents.Controller;

namespace Game.Scripts.MenuComponents
{
    [CreateAssetMenu(fileName = "CanvasFactory", menuName = "CanvasFactory")]
    public class CanvasFactory : ScriptableObject
    {
        [SerializeField] private Canvas _meleeCanvas;
        [SerializeField] private Canvas _rangeCanvas;

        private Canvas _canvas;
        private bool _isJoystickActive;

        public Joystick MovementJoystick { get; private set; }
        public Joystick RotationJoystick { get; private set; }

        public Canvas Create(CharacterType characterType, Player player)
        {
            Canvas prefab = GetPrefab(characterType);
            Canvas canvas = Instantiate(prefab);
            PlayerDataViewer healthViewer = canvas.GetComponent<PlayerDataViewer>();

            healthViewer.Init(player);

            AbilityWindowImprovement windowImprovement = canvas.GetComponent<AbilityWindowImprovement>();
            AbilityViewer abilityViewer = canvas.GetComponent<AbilityViewer>();
            AbilityButtonsInitializer abilityButtonsInitializer = canvas.GetComponent<AbilityButtonsInitializer>();

            windowImprovement?.Init(player);
            abilityViewer?.Init(player);
            abilityButtonsInitializer?.InitButtons(player);

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