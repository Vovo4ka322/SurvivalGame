using UnityEngine;

namespace Game.Scripts.EnemyComponents.Animations
{
    public class AnimatorParameterChecker
    {
        private readonly Animator _animator;

        public AnimatorParameterChecker(Animator animator)
        {
            _animator = animator;
        }

        public bool HasParameter(int parameterHash)
        {
            foreach (var parameter in _animator.parameters)
            {
                if (parameter.nameHash == parameterHash)
                {
                    return true;
                }
            }

            return false;
        }
    }
}