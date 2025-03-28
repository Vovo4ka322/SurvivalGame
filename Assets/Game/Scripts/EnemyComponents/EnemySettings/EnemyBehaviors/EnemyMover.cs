using UnityEngine;
using UnityEngine.AI;
using Game.Scripts.EnemyComponents.Animations;
using Game.Scripts.EnemyComponents.Interfaces;

namespace Game.Scripts.EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class EnemyMover : IEnemyMovement
    {
        private readonly EnemyAnimationState _animationState;
        private readonly NavMeshAgent _agent;

        private bool _canMove = true;

        public EnemyMover(EnemyAnimationState animator, NavMeshAgent agent, float moveSpeed)
        {
            _animationState = animator;
            _agent = agent;
            _agent.speed = moveSpeed;
        }

        public void ProcessMovement(Vector3 targetPosition, bool spawnCompleted, bool isAttacking)
        {
            if (!spawnCompleted || isAttacking || !_canMove)
            {
                Stop();
                return;
            }

            _agent.SetDestination(targetPosition);
            StartMoving();
        }

        public void CanMove(bool value)
        {
            _canMove = value;
        }

        public void StartMoving()
        {
            _animationState.Move(true);
        }

        public void Stop()
        {
            _animationState.Move(false);
            _agent.ResetPath();
        }
    }
}