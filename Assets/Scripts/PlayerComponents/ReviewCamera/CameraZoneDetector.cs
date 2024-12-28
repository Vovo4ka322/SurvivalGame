using System.Collections.Generic;
using UnityEngine;
namespace PlayerComponents.ReviewCamera
{
    public class CameraZoneDetector : MonoBehaviour
    {
        [SerializeField] private CameraFollower _cameraFollower;
        
        private List<CameraTriggerZone> _activeZones;

        private void Start()
        {
            _activeZones = new List<CameraTriggerZone>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CameraTriggerZone>(out var triggerZone)) 
            {
                Enter(triggerZone);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<CameraTriggerZone>(out var triggerZone))
            {
                Exit(triggerZone);
            }
        }

        private void Enter(CameraTriggerZone triggerZone)
        {
            _activeZones.Add(triggerZone);
            UpdateRotation();
        }

        private void Exit(CameraTriggerZone triggerZone)
        {
            if (_activeZones.Remove(triggerZone))
            {
                UpdateRotation();
            }
        }

        private void UpdateRotation()
        {
            if(_activeZones.Count > 0) 
            {
                CameraTriggerZone topZone = _activeZones[_activeZones.Count - 1];
                _cameraFollower.SetTargetRotation(topZone.TargetRotation);
            } 
            else 
            {
                _cameraFollower.Reset();
            }
        }
    }
}