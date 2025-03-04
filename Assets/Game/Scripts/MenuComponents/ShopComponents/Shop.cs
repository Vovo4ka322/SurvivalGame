using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.MenuComponents.ShopComponents.Buttons;
using Game.Scripts.MenuComponents.ShopComponents.Data;
using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;
using Game.Scripts.MenuComponents.ShopComponents.Viewers;
using Game.Scripts.MenuComponents.ShopComponents.Visitors;
using Game.Scripts.MenuComponents.ShopComponents.WalletComponents;

namespace Game.Scripts.MenuComponents.ShopComponents
{
    public class Shop : MonoBehaviour
    {
        [Header("Content store")]
        [SerializeField] private ShopContent _contentItems;
        [SerializeField] private ShopPanel _shopPanel;
        [SerializeField] private ShopCategoryButton _characterSkinsButton;
        [SerializeField] private BuffShop _buffShop;

        [Header("UI elements")]
        [SerializeField] private BuyButton _buyButton;
        [SerializeField] private Button _selectionButton;
        [SerializeField] private Image _selectedText;
        [SerializeField] private GameObject _meleeDescription;
        [SerializeField] private GameObject _rangeDescription;

        [Header("Positioning model")]
        [SerializeField] private SkinPlacement _skinPlacement;
        [SerializeField] private Camera _modelCamera;
        [SerializeField] private Transform _characterCategoryCameraPosition;

        private IDataSaver _iDataSaver;
        private ShopItemView _previewedItem;
        private Wallet _wallet;

        private SkinSelector _skinSelector;
        private SkinUnlocker _skinUnlocker;
        private OpenSkinsChecker _openSkinsChecker;
        private SelectedSkinChecker _selectedSkinChecker;

        private void OnEnable()
        {
            _shopPanel.ItemViewClicked += OnItemViewClicked;
            _buyButton.Click += OnBuyButtonClick;
            _selectionButton.onClick.AddListener(OnSelectionButtonClick);
            _characterSkinsButton.Click += OnCharacterSkinsButtonClick;

            _openSkinsChecker.RangeCharacterOpened += ShowRangeDesctiprion;
            _openSkinsChecker.MeleeCharacterOpened += ShowMeleeDescription;
        }

        private void OnDisable()
        {
            _shopPanel.ItemViewClicked -= OnItemViewClicked;
            _buyButton.Click -= OnBuyButtonClick;
            _selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
            _characterSkinsButton.Click -= OnCharacterSkinsButtonClick;

            _openSkinsChecker.RangeCharacterOpened -= ShowRangeDesctiprion;
            _openSkinsChecker.MeleeCharacterOpened -= ShowMeleeDescription;
        }

        public void Initialize(IDataSaver iDataSaver, Wallet wallet, OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker, SkinSelector skinSelector, SkinUnlocker skinUnlocker, IPersistentData persistentData)
        {
            _wallet = wallet;
            _openSkinsChecker = openSkinsChecker;
            _selectedSkinChecker = selectedSkinChecker;
            _skinSelector = skinSelector;
            _skinUnlocker = skinUnlocker;
            _iDataSaver = iDataSaver;

            _shopPanel.Initialize(openSkinsChecker, selectedSkinChecker);
            _shopPanel.ItemViewClicked += OnItemViewClicked;
            _buffShop.Init(_wallet, _iDataSaver, persistentData);

            OnCharacterSkinsButtonClick();
        }

        private void ShowMeleeDescription()
        {
            _rangeDescription.SetActive(false);
            _meleeDescription.SetActive(true);
        }

        private void ShowRangeDesctiprion()
        {
            _meleeDescription.SetActive(false);
            _rangeDescription.SetActive(true);
        }

        private void OnItemViewClicked(ShopItemView item)
        {
            _previewedItem = item;
            _skinPlacement.InstantiateModel(_previewedItem.Model);
            _openSkinsChecker.Visit(_previewedItem.Item);

            if(_openSkinsChecker.IsOpened)
            {
                _selectedSkinChecker.Visit(_previewedItem.Item);

                if(_selectedSkinChecker.IsSelected)
                {
                    ShowSelectedText();

                    return;
                }

                ShowSelectionButton();
            }
            else
            {
                ShowBuyButton(_previewedItem.Price);
            }
        }

        private void OnBuyButtonClick()
        {
            if(_wallet.IsEnough(_previewedItem.Price))
            {
                _wallet.Spend(_previewedItem.Price);
                _skinUnlocker.Visit(_previewedItem.Item);

                SelectSkin();

                _previewedItem.Unlock();
                _iDataSaver.Save();
            }
        }

        private void OnSelectionButtonClick()
        {
            SelectSkin();

            _iDataSaver.Save();
        }

        private void UpdateCameraTransform(Transform transform)
        {
            _modelCamera.transform.position = transform.position;
            _modelCamera.transform.rotation = transform.rotation;
        }

        private void SelectSkin()
        {
            _skinSelector.Visit(_previewedItem.Item);
            _shopPanel.Select(_previewedItem);

            ShowSelectedText();
        }

        private void ShowSelectionButton()
        {
            _selectionButton.gameObject.SetActive(true);

            HideBuyButton();
            HideSelectedText();
        }

        private void ShowSelectedText()
        {
            _selectedText.gameObject.SetActive(true);

            HideSelectionButton();
            HideBuyButton();
        }

        private void ShowBuyButton(int price)
        {
            _buyButton.gameObject.SetActive(true);
            _buyButton.UpdateText(price);

            if(_wallet.IsEnough(price))
            {
                _buyButton.Unlock();
            }
            else
            {
                _buyButton.Lock();
            }

            HideSelectedText();
            HideSelectionButton();
        }

        private void HideBuyButton() => _buyButton.gameObject.SetActive(false);

        private void HideSelectionButton() => _selectionButton.gameObject.SetActive(false);

        private void HideSelectedText() => _selectedText.gameObject.SetActive(false);

        private void OnCharacterSkinsButtonClick()
        {
            _characterSkinsButton.Select();

            UpdateCameraTransform(_characterCategoryCameraPosition);

            _shopPanel.Show(_contentItems.CharacterSkinItems.Cast<ShopItem>());
        }
    }
}
