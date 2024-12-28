using UnityEngine;

namespace Player.Animations
{
    public static class AnimationDataParams
    {
        public static class Params
        {
            private const string Horizontal = "horizontal";
            private const string Vertical = "vertical";

            public static readonly int HorizontalHash = Animator.StringToHash(Horizontal);
            public static readonly int VerticalHash = Animator.StringToHash(Vertical);
        }
    }
}