using UnityEngine;
using Game.Scripts.MenuComponents.ShopComponents.SkinComponents;

namespace Game.Scripts.MenuComponents.ShopComponents
{
    public abstract class ShopItem : ScriptableObject
    {
        [field: SerializeField] public SkinModel Model { get; private set; }
        [field: SerializeField] public Sprite Image { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
    }
}