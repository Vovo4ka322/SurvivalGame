using UnityEngine;

namespace EnemyComponents.EnemySettings.EnemyBehaviors
{
    public class EnemyRotation
    {
        private readonly Transform _transform;
        private readonly float _rotationSpeed;

        public EnemyRotation(Transform transform, float rotationSpeed)
        {
            _transform = transform;
            _rotationSpeed = rotationSpeed;
        }

        public void RotateTowards(Vector3 targetPosition)
        {
            Vector3 direction = (targetPosition - _transform.position).normalized;

            if(direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                float t = 1f - Mathf.Exp(-_rotationSpeed * Time.deltaTime);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, t);
            }
        }
    }
}