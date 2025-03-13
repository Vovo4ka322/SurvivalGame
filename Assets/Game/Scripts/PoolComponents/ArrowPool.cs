using Unity.Entities;
using UnityEngine;

namespace Game.Scripts.PoolComponents
{
    public class ArrowPool : BasePool<Arrow>
    {
        public ArrowPool(Arrow arrow, PoolSettings settings, Transform container = null) : base(arrow, settings, container) { }
    }
}