using UnityEngine;
namespace PlayerComponents.ReviewCamera
{
    [RequireComponent(typeof(Collider))]
    public class CameraTriggerZone : MonoBehaviour
    {
        [SerializeField] private Vector3 _cameraRotationEuler;
        
        private Collider _collider;
        public Quaternion TargetRotation => Quaternion.Euler(_cameraRotationEuler);
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }
        
        private void Reset()
        {
            if(_collider != null && !_collider.isTrigger) 
            {
                _collider.isTrigger = true;
            }
        }
    }
}