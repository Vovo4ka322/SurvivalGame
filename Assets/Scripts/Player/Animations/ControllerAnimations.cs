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
        
        public void UpdateAnimations(float movementMagnitude, Vector3 moveDirection)
        {
            _animator.SetFloat(AnimationDataParams.Params.Forward, movementMagnitude);

            if(movementMagnitude > 0.1) 
            {
                float angle = Vector3.SignedAngle(transform.forward, moveDirection, Vector3.up);
                _animator.SetFloat(AnimationDataParams.Params.Angle, angle);
            } 
            else 
            {
                _animator.SetFloat(AnimationDataParams.Params.Angle, 0);
            }
        }
    }
}