using Cinemachine;
using PlayerComponents.Animations;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerComponents.Controller
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private ControllerAnimations _controllerAnimations;
        [SerializeField] private PlayerController _controller;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private LayerMask _layerMask;

        private float _moveSpeed;
        private Camera _camera;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _camera = Camera.main;
        }
        
        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        public void Init(float movementSpeed)
        {
            _moveSpeed = movementSpeed;
        }

        public void ChangeMoveSpeed(float amount)
        {
            _moveSpeed += amount;
        }

        private void HandleMovement()
        {
            float horizontal = _controller.Movement.x;
            float vertical = _controller.Movement.y;

            Vector3 forward = _camera.transform.forward;
            Vector3 right = _camera.transform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            
            _moveDirection = forward * vertical + right * horizontal;
            _moveDirection = _moveDirection.normalized;
            
            float speedDelta = _moveSpeed * Time.deltaTime;
            transform.position += _moveDirection * speedDelta;
            
            _controllerAnimations.PlayMove(_moveDirection);
        }

        Vector3 _dir;

        private void HandleRotation()
        {
            Ray ray = _camera.ScreenPointToRay(_controller.Rotation);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _layerMask))
            {
                Vector3 direction = hitInfo.point - transform.position;
                direction.y = 0;

                _dir = hitInfo.point;

                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(_dir, 0.5f);
        }
    }
}