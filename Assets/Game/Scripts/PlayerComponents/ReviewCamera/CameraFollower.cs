using UnityEngine;

namespace Game.Scripts.PlayerComponents.ReviewCamera
{
    public class CameraFollower : MonoBehaviour
    {
        [Header("Moving Settings")]
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Vector3 _offsetPosition;
        [SerializeField] private float _moveSpeed;

        [Header("Rotation Settings")] 
        [SerializeField] private Vector3 _defaultRotationEuler;
        [SerializeField] private float _rotationSpeed = 5f;
        
        private Quaternion _targetRotation;
        private Quaternion _defaultRotation;
        private Quaternion _currentRotation;
        
        private void Awake()
        {
            _defaultRotation = Quaternion.Euler(_defaultRotationEuler);
            _targetRotation = _defaultRotation;
            _currentRotation = _defaultRotation;
        }

        private void Update()
        {
            MoveAndRotate();
        }

        private void MoveAndRotate()
        {
            _currentRotation = Quaternion.Slerp(_currentRotation, _targetRotation, _rotationSpeed * Time.deltaTime);
            Vector3 rotatedOffset = _currentRotation * _offsetPosition;
            Vector3 desiredPosition = _playerTransform.position + rotatedOffset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, _moveSpeed * Time.deltaTime);
            
            transform.LookAt(_playerTransform);
        }

        public void SetTargetRotation(Quaternion rotation)
        {
            _targetRotation = rotation;
        }

        public void Reset()
        {
            _targetRotation = _defaultRotation;
        }
    }
}