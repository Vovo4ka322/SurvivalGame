using UnityEngine;
using Game.Scripts.PlayerComponents.Animations;

namespace Game.Scripts.PlayerComponents.Controller
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private ControllerAnimations _controllerAnimations;
        [SerializeField] private PlayerController _controller;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private LayerMask _layerMask;

        [SerializeField] private Joystick _joystickForMovement;
        [SerializeField] private Joystick _joystickForRotation;
        [SerializeField] private bool _isJoystickActive;

        private float _moveSpeed;
        private Camera _camera;
        private Vector3 _moveDirection;
        private bool _isSkillWorking;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        public void SetStateSkillWorkingTrue() => _isSkillWorking = true;

        public void SetStateSkillWorkingFalse() => _isSkillWorking = false;

        public void InitJoysticks(bool isJoystickActive, Joystick joystickForMovement, Joystick joystickForRotation)
        {
            _isJoystickActive = isJoystickActive;
            _joystickForMovement = joystickForMovement;
            _joystickForRotation = joystickForRotation;
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
            float horizontal;
            float vertical;

            if (_isJoystickActive)
            {
                horizontal = _joystickForMovement.Horizontal;
                vertical = _joystickForMovement.Vertical;
            }
            else
            {
                horizontal = _controller.Movement.x;
                vertical = _controller.Movement.y;
            }

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

        private void HandleRotation()
        {
            if (_isSkillWorking)
            {
                return;
            }

            Vector3 direction = Vector3.zero;

            if (_isJoystickActive)
            {
                direction = new Vector3(_joystickForRotation.Horizontal, 0, _joystickForRotation.Vertical);
            }
            else
            {
                Ray ray = _camera.ScreenPointToRay(_controller.Rotation);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, _layerMask))
                {
                    direction = hitInfo.point - transform.position;
                    direction.y = 0f;
                }
            }

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
            }
        }
    }
}