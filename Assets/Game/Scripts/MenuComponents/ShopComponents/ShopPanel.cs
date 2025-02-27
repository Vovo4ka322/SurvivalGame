using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.Scripts.MenuComponents.ShopComponents.Viewers;
using Game.Scripts.MenuComponents.ShopComponents.Visitors;

namespace Game.Scripts.MenuComponents.ShopComponents
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private ShopItemViewFactory _shopItemViewFactory;
        
        private OpenSkinsChecker _openSkinsChecker;
        private SelectedSkinChecker _selectedSkinChecker;
        private List<ShopItemView> _shopItems = new List<ShopItemView>();
        
        public event Action<ShopItemView> ItemViewClicked;
        
        public void Initialize(OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker)
        {
            _openSkinsChecker = openSkinsChecker;
            _selectedSkinChecker = selectedSkinChecker;
        }
        
        public void Show(IEnumerable<ShopItem> items)
        {
            Clear();
            
            foreach(ShopItem item in items)
            {
                ShopItemView spawnedItem = _shopItemViewFactory.Get(item, _itemsParent);
                
                spawnedItem.Click += OnItemViewClick;
                
                spawnedItem.Unselect();
                spawnedItem.UnHighlight();
                
                _openSkinsChecker.Visit(spawnedItem.Item);
                
                if(_openSkinsChecker.IsOpened)
                {
                    _selectedSkinChecker.Visit(spawnedItem.Item);
                    
                    if(_selectedSkinChecker.IsSelected)
                    {
                        spawnedItem.Select();
                        spawnedItem.Highlight();
                        
                        ItemViewClicked?.Invoke(spawnedItem);
                    }
                    
                    spawnedItem.Unlock();
                }
                else
                {
                    spawnedItem.Lock();
                }
                
                _shopItems.Add(spawnedItem);
            }
            
            Sort();
        }
        
        public void Select(ShopItemView itemView)
        {
            foreach(ShopItemView item in _shopItems)
            {
                item.Unselect();
            }
            
            itemView.Select();
        }
        
        private void Sort()
        {
            _shopItems = _shopItems.OrderBy(item => item.IsLock).ThenByDescending(item => item.Price).ToList();
            
            for(int i = 0; i < _shopItems.Count; i++)
            {
                _shopItems[i].transform.SetSiblingIndex(i);
            }
        }
        
        private void OnItemViewClick(ShopItemView itemView)
        {
            Highlight(itemView);
            
            ItemViewClicked?.Invoke(itemView);
        }
        
        private void Highlight(ShopItemView shopItemView)
        {
            foreach(ShopItemView item in _shopItems)
            {
                item.UnHighlight();
            }
            
            shopItemView.Highlight();
        }
        
        private void Clear()
        {
            foreach(ShopItemView item in _shopItems)
            {
                item.Click -= OnItemViewClick;
                
                Destroy(item.gameObject);
            }
            
            _shopItems.Clear();
        }
    }
}