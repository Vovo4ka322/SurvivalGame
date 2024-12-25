using UnityEngine;

namespace Player.Animations
{
    public static class AnimationDataParams
    {
        public static class Params
        {
            private const string RunForward = "runForward";
            private const string MoveAngle = "moveAngle";
    
            public static readonly int Forward = Animator.StringToHash(RunForward);
            public static readonly int Angle = Animator.StringToHash(MoveAngle);
        }
    }
}