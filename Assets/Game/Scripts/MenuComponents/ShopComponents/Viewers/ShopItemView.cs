using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Scripts.MenuComponents.ShopComponents.Common;
using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;

namespace Game.Scripts.MenuComponents.ShopComponents.Viewers
{
    [RequireComponent(typeof(Image))]
    public class ShopItemView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Sprite _standardBackground;
        [SerializeField] private Sprite _highlightBackground;
        [SerializeField] private Image _contentImage;
        [SerializeField] private Image _lockImage;
        [SerializeField] private Image _selectionText;
        [SerializeField] private IntValueView _priceView;

        private Image _backgroundImage;
        
        public CharacterSkinItem Item { get; private set; }
        public bool IsLock { get; private set; }
        public int Price => Item.Price;
        public SkinModel Model => Item.Model;
        
        public event Action<ShopItemView> Click;

        public void Initialize(CharacterSkinItem item)
        {
            _backgroundImage = GetComponent<Image>();
            
            Item = item;
            _backgroundImage.sprite = _standardBackground;
            _contentImage.sprite = item.Image;
            
            _priceView.Show(Price);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Click?.Invoke(this);
        }

        public void Lock()
        {
            IsLock = true;
            
            _lockImage.gameObject.SetActive(IsLock);
            _priceView.Show(Price);
        }

        public void Unlock()
        {
            IsLock = false;
            
            _lockImage.gameObject.SetActive(IsLock);
            _priceView.Hide();
        }

        public void Select()
        {
            _selectionText.gameObject.SetActive(true);
        }

        public void Unselect()
        {
            _selectionText.gameObject.SetActive(false);
        }

        public void Highlight()
        {
            _backgroundImage.sprite = _highlightBackground;
        }

        public void UnHighlight()
        {
            _backgroundImage.sprite = _standardBackground;
        }
    }
}