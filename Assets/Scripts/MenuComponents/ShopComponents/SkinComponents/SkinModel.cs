using UnityEngine;

namespace MenuComponents.ShopComponents.SkinComponents
{
    [RequireComponent(typeof(Animator))]
    public class SkinModel : MonoBehaviour
    {
        private const string IdleAnimation = "Idle";
        private const string AttackAnimation = "isAttack";
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayIdle()
        {
            _animator.SetBool(AttackAnimation, false);
            _animator.Play(IdleAnimation);
        }
    }
}