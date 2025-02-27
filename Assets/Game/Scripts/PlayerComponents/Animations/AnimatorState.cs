using UnityEngine;

namespace Game.Scripts.PlayerComponents.Animations
{
    public class AnimatorState : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public readonly int CanUseSkill1Hash = Animator.StringToHash("canUseSkill1");
        public readonly int IsAttack = Animator.StringToHash("isAttack");
        public readonly int Speed = Animator.StringToHash("Speed");
        public readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");

        private readonly float _baseValue = 1f;

        public void SetTrueBoolState(int id)
        {
            _animator.SetBool(id, true);
        }

        public void SetFalseBoolState(int id)
        {
            _animator.SetBool(id, false);
        }

        public void SetFloatValue(int id, float value)
        {
            _animator.SetFloat(id, _baseValue + value);
        }
    }
}