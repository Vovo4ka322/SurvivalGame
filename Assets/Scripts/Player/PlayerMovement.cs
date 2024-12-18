using UnityEngine;

namespace MainPlayer
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _turnSpeed;
        [SerializeField] private PlayerController _controller;

        [field: SerializeField] public float _moveSpeed { get; private set; }

        private Vector3 _moveDirection;

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
            float horizontal = _controller.Movement.x;
            float vertical = _controller.Movement.y;

            _moveDirection = new Vector3(horizontal, 0, vertical).normalized;
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
            Vector3 rotatedDirection = rotationMatrix.MultiplyPoint3x4(_moveDirection);
            Vector3 move = rotatedDirection * _moveSpeed * Time.deltaTime;
            transform.position += move;
        }

        private void HandleRotation()
        {
            Ray ray = Camera.main.ScreenPointToRay(_controller.Rotation);
            Plane plane = new(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out float rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                Vector3 direction = point - transform.position;
                direction.y = 0;

                if (direction != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _turnSpeed);
                }
            }
        }
    }
}