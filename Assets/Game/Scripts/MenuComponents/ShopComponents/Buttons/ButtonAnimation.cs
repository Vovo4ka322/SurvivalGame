using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.Scripts.MenuComponents.ShopComponents.Buttons
{
    public class ButtonAnimation : MonoBehaviour
    {
        [SerializeField] private float _pressedScaleFactor = 0.9f;
        [SerializeField] private float _animationDuration = 0.1f;
        [SerializeField] private Vector3 _shakeStrength = new Vector3(0.1f, 0.1f, 0f);
        [SerializeField] private int _shakeVibrato = 10;
        [SerializeField] private float _shakeRandomness = 90f;

        [SerializeField] private ParticleSystem _buffParticle;
        
        private readonly Dictionary<Transform, Vector3> _childOriginalScales = new Dictionary<Transform, Vector3>();
        
        private Vector3 _originalParentScale;
        
        private void Awake()
        {
            if (_buffParticle != null && ! _buffParticle.gameObject.scene.isLoaded)
            {
                _buffParticle = Instantiate(_buffParticle);
            }
        }
        
        public void SetSelectedBuffButton(ref Button currentSelectedBuffButton, Button newButton)
        {
            if(newButton == null || currentSelectedBuffButton == newButton)
            {
                return;
            }
            
            if(_buffParticle != null)
            {
                _buffParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                _buffParticle.transform.SetParent(newButton.transform, false);
                _buffParticle.transform.localPosition = Vector3.zero;
                _buffParticle.Play();
            }

            currentSelectedBuffButton = newButton;
        }
        
        public void PlayPressedAnimation(Button button)
        {
            if(button == null)
            {
                return;
            }

            Transform buttonTransform = button.transform;
            buttonTransform.DOKill(true);
            _originalParentScale = buttonTransform.localScale;
            buttonTransform.localScale = _originalParentScale;
            SaveChildrenScales(buttonTransform);
            
            buttonTransform.DOScale(_originalParentScale * _pressedScaleFactor, _animationDuration)
                .OnUpdate(() => CompensateChildrenScales(buttonTransform))
                .OnComplete(() =>
                {
                    buttonTransform.DOScale(_originalParentScale, _animationDuration)
                        .OnUpdate(() => CompensateChildrenScales(buttonTransform))
                        .OnComplete(() => RestoreChildrenScales());
                });
        }

        public void PlayTryPressedAnimation(Button button)
        {
            if(button == null)
            {
                return;
            }

            Transform buttonTransform = button.transform;
            buttonTransform.DOKill(true);
            _originalParentScale = buttonTransform.localScale;
            buttonTransform.localScale = _originalParentScale;
            SaveChildrenScales(buttonTransform);
            
            buttonTransform.DOScale(_originalParentScale * _pressedScaleFactor, _animationDuration)
                .OnUpdate(() => CompensateChildrenScales(buttonTransform))
                .OnComplete(() =>
                {
                    buttonTransform.DOShakeScale(_animationDuration, _shakeStrength, _shakeVibrato, _shakeRandomness)
                        .OnUpdate(() => CompensateChildrenScales(buttonTransform))
                        .OnComplete(() =>
                        {
                            buttonTransform.DOScale(_originalParentScale, _animationDuration)
                                .OnUpdate(() => CompensateChildrenScales(buttonTransform))
                                .OnComplete(() => RestoreChildrenScales());
                        });
                });
        }
        
        private void SaveChildrenScales(Transform parent)
        {
            _childOriginalScales.Clear();
            
            foreach (Transform child in parent)
            {
                _childOriginalScales.Add(child, child.localScale);
            }
        }
        
        private void CompensateChildrenScales(Transform parent)
        {
            Vector3 currentScale = parent.localScale;
            float scaleFactor = Mathf.Approximately(currentScale.x, 0f) ? 1f : _originalParentScale.x / currentScale.x;
        
            foreach (Transform child in parent)
            {
                if (_childOriginalScales.TryGetValue(child, out Vector3 originalChildScale))
                {
                    child.localScale = originalChildScale * scaleFactor;
                }
            }
        }
        
        private void RestoreChildrenScales()
        {
            foreach (var kvp in _childOriginalScales)
            {
                kvp.Key.localScale = kvp.Value;
            }
            
            _childOriginalScales.Clear();
        }
    }
}