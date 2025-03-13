using System;
using UnityEngine;
using TMPro;

namespace Game.Scripts.MenuComponents.ShopComponents.Common
{
    public class ValueView<T> : MonoBehaviour where T : IConvertible
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void Show(T value)
        {
            gameObject.SetActive(true);
            _text.text = value.ToString();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}