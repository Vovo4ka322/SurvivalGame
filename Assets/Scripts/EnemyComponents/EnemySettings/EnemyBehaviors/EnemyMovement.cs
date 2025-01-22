using UnityEngine;
using EnemyComponents.Animations;

namespace EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class EnemyMovement
    {
        private readonly EnemyAnimationController _animationController;
        private readonly EnemyRotation _rotation;
        private readonly Transform _transform;
        private readonly float _moveSpeed;
        private bool _canMove = true;
        
        public EnemyMovement(Transform transform, float moveSpeed, EnemyAnimationController animator, float rotationSpeed)
        {
            _transform = transform;
            _moveSpeed = moveSpeed;
            _animationController = animator;
            _rotation = new EnemyRotation(transform, rotationSpeed);
        }
        
        public void Move(Vector3 targetPosition)
        {
            if(!_canMove)
            {
                return;
            }
            
            Vector3 oldPosition = _transform.position;
            _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, _moveSpeed * Time.deltaTime);
            _rotation.RotateTowards(targetPosition);
            Vector3 direction = _transform.position - oldPosition;
            
            if (direction.sqrMagnitude > Mathf.Epsilon)
            {
                PlayMove();
            }
            else
            {
                StopMove();
            }
        }

        public void PlayMove()
        {
            _canMove = true;
            _animationController.Move(true);
        }

        public void StopMove()
        {
            _canMove = false;
            _animationController.Move(false);
        }
    }
}