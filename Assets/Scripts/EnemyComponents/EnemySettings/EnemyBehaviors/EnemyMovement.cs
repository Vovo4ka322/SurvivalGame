using UnityEngine;
using EnemyComponents.Animations;
using EnemyComponents.Interfaces;

namespace EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class EnemyMovement : IEnemyMovement
    {
        private readonly EnemyAnimationController _animationController;
        private readonly Transform _transform;
        private readonly float _moveSpeed;
        
        private bool _canMove = true;
        
        public EnemyMovement(Transform transform, float moveSpeed, EnemyAnimationController animator)
        {
            _transform = transform;
            _moveSpeed = moveSpeed;
            _animationController = animator;
        }
        
        public void Move(Vector3 targetPosition)
        {
            if(!_canMove)
            {
                return;
            }
            
            Vector3 oldPosition = _transform.position;
            _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, _moveSpeed * Time.deltaTime);
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