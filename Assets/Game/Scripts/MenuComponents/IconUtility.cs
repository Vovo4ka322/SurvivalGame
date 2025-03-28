using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.MenuComponents
{
    public class IconUtility
    {
        public void SetIconDimmed(Image icon, bool dimmed)
        {
            if (icon == null)
                return;

            Color color = icon.color;
            color.a = dimmed ? 0.5f : 1f;
            icon.color = color;
        }
    }
}