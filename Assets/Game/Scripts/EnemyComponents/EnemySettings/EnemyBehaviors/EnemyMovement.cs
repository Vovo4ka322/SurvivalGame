using UnityEngine;
using UnityEngine.AI;
using Game.Scripts.EnemyComponents.Animations;
using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class EnemyMovement : IEnemyMovement
    {
        private readonly EnemyAnimationController _animationController;
        private readonly Transform _transform;
        private readonly NavMeshAgent _agent;
        private readonly float _moveSpeed;
        
        private bool _canMove = true;

        public float MovementPredictionThreshold { get; private set; } = 0;

        public float MovementPredictionTime { get; private set; } = 1f;

        public bool UseMovementPrediction { get; private set; }

        public EnemyMovement(Transform transform, float moveSpeed, EnemyAnimationController animator, NavMeshAgent agent)
        {
            _transform = transform;
            _moveSpeed = moveSpeed;
            _animationController = animator;
            _agent = agent;
            _agent.speed = _moveSpeed;
        }

        public bool IsMoveAllowed => _canMove;

        public void Move(Vector3 targetPosition)
        {
            if (!_canMove)
            {
                return;
            }
            
            _agent.SetDestination(targetPosition);
        }

        //public void Move(Vector3 targetPosition)
        //{
        //    if (!_canMove)
        //    {
        //        return;
        //    }

        //    Vector3 oldPosition = _transform.position;
        //    _transform.position = Vector3.MoveTowards(_transform.position, targetPosition, _moveSpeed * Time.deltaTime);
        //    Vector3 direction = _transform.position - oldPosition;

        //    if (direction.sqrMagnitude > Mathf.Epsilon)
        //    {
        //        PlayMove();
        //    }
        //    else
        //    {
        //        StopMove();
        //    }
        //}

        public void CanMove(bool value)
        {
            _canMove = value;
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