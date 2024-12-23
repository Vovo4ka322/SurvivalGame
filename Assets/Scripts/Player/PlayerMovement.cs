using Controller;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerController _controller;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _moveSpeed;

        private Vector3 _moveDirection;
        private Transform _cameraTransform;

        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        public void ChangeMoveSpeed(float amount)
        {
            _moveSpeed += amount;
        }

        private void HandleMovement()
        {
            if(_cameraTransform == null) 
            {
                return;
            }
            
            float horizontal = _controller.Movement.x;
            float vertical = _controller.Movement.y;
            
            Vector3 forward = _cameraTransform.forward;
            Vector3 right = _cameraTransform.right;
            
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            
            _moveDirection = forward * vertical + right * horizontal;
            _moveDirection = _moveDirection.normalized;
            Vector3 move = _moveDirection * _moveSpeed * Time.deltaTime;
            transform.position += move;
        }

        private void HandleRotation()
        {
            if (_moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
            }
        }
    }
}