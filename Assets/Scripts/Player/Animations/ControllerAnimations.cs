using UnityEngine;

namespace Player.Animations
{
    [RequireComponent(typeof(Animator))]
    public class ControllerAnimations: MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        public void PlayMove(Vector3 moveDirection)
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(moveDirection);
            SetMoveParams(relativeDirection.x, relativeDirection.z);
        }

        private void SetMoveParams(float horizontal, float vertical)
        {
            _animator.SetFloat(AnimationDataParams.Params.HorizontalHash, horizontal);
            _animator.SetFloat(AnimationDataParams.Params.VerticalHash, vertical);
        }
    }
}