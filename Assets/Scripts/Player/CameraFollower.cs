using UnityEngine;

namespace Player
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offsetPosition;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _smooth;

        private Vector3 _targetPosotion;
        private Vector3 _velocity = Vector3.zero;

        private void LateUpdate()
        {
            Move();
        }

        private void Move()
        {
            _targetPosotion = _target.transform.position + _offsetPosition;
            transform.position = Vector3.SmoothDamp(transform.position, _targetPosotion, ref _velocity, _smooth);
        }
    }
}
