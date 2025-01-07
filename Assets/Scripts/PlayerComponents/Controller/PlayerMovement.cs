using PlayerComponents.Animations;
using UnityEngine;

namespace PlayerComponents.Controller
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private ControllerAnimations _controllerAnimations;
        [SerializeField] private float _turnSpeed;
        [SerializeField] private float _moveSpeed;
        
        private Camera _camera;
        private PlayerInput _playerInput;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _camera = Camera.main;
            _playerInput = new PlayerInput();
        }
        
        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void Start()
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }

        public void ChangeMoveSpeed(float amount)
        {
            _moveSpeed += amount;
        }

        private void HandleMovement()
        {
            Vector2 input = _playerInput.Player.Move.ReadValue<Vector2>();
            float horizontal = input.x;
            float vertical = input.y;
            
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
            
            //_controllerAnimations.PlayMove(_moveDirection);
        }

        private void HandleRotation()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out float rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                Vector3 direction = point - transform.position;
                direction.y = 0;

                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
                }
            }
        }
    }
}