using UnityEngine;
using MenuComponents.ShopComponents.SkinComponents;

namespace MenuComponents.ShopComponents
{
    public abstract class ShopItem : ScriptableObject
    {
        [field: SerializeField] public SkinModel Model { get; private set; }
        [field: SerializeField] public Sprite Image { get; private set; }
        [field: SerializeField, Range(0, 10000)] public int Price { get; private set; }
    }
}