using EnemyComponents.Animations;
using EnemyComponents.Interfaces;
using Pathfinding;
using UnityEngine;

namespace EnemyComponents
{
    [RequireComponent(typeof(Seeker))]
    public class EnemyAStarMovement : IEnemyMovement
    {
        private readonly EnemyAnimationController _animationController;
        private readonly Transform _transform;
        private readonly Seeker _seeker;
        private readonly float _moveSpeed;
        private readonly float _rotationSpeed;
        private readonly float _pathRequestInterval = 1f;
        private readonly float _nextWaypointDistance = 1.5f;
        
        private Path _path;
        private int _currentWaypoint;
        private float _lastPathRequestTime = 0f;

        public EnemyAStarMovement(Transform transform, float moveSpeed, float rotationSpeed, EnemyAnimationController animationController)
        {
            _transform = transform;
            _moveSpeed = moveSpeed;
            _rotationSpeed = rotationSpeed;
            _animationController = animationController;

            _seeker = _transform.GetComponent<Seeker>();

            if(_seeker == null)
            {
                _seeker = _transform.gameObject.AddComponent<Seeker>();
            }

            _currentWaypoint = 0;
        }

        private void SetTarget(Vector3 targetPosition)
        {
            if(Time.time - _lastPathRequestTime < _pathRequestInterval)
                return;

            NNInfo nearestInfo = AstarPath.active.GetNearest(targetPosition);
            Vector3 target = (nearestInfo.node != null) ? (Vector3)nearestInfo.position : targetPosition;

            if(_seeker.IsDone())
            {
                _seeker.StartPath(_transform.position, target, OnPathComplete);
                _lastPathRequestTime = Time.time;
            }
        }

        private void OnPathComplete(Path p)
        {
            if(!p.error)
            {
                _path = p;
                _currentWaypoint = 0;
            }
            else
            {
                _path = null;
            }
        }

        public void Move(Vector3 targetPosition)
        {
            SetTarget(targetPosition);
            
            if(_path == null || _currentWaypoint >= _path.vectorPath.Count)
            {
                _animationController.Move(false);

                return;
            }
            
            Vector3 waypoint = _path.vectorPath[_currentWaypoint];
            Vector3 direction = (waypoint - _transform.position).normalized;
            Vector3 velocity = direction * _moveSpeed;
            
            _transform.position += velocity * Time.deltaTime;
            
            if(direction.sqrMagnitude > Mathf.Epsilon)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
            
            if(Vector3.Distance(_transform.position, waypoint) < _nextWaypointDistance)
            {
                _currentWaypoint++;
            }

            if(velocity.sqrMagnitude > Mathf.Epsilon)
            {
                _animationController.Move(true);
            }
            else
            {
                _animationController.Move(false);
            }
        }

        public void PlayMove()
        {
            _animationController.Move(true);
        }

        public void StopMove()
        {
            _animationController.Move(false);
            _path = null;
        }
    }
}
