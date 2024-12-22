using MainPlayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotUser : MonoBehaviour, ICooldownable
{
    [SerializeField] private ArrowSpawner _arrowSpawner;

    private Multishot _multishotScriptableObject;
    private float _lastUsedTimer = 0;
    private bool _canUseFirstTime = true;
    private int _numberOfArrows = 3;
    private float _spreadAngle = 15f;

    public float CooldownTime { get; private set; }

    public void Upgrade(Multishot multishot)
    {
        _multishotScriptableObject = multishot;
    }

    public IEnumerator UseAbility()
    {
        Debug.Log(_multishotScriptableObject.CooldownTime + " Cooldown");
        float duration = 0;

        if (Time.time >= _lastUsedTimer + _multishotScriptableObject.CooldownTime || _canUseFirstTime)
        {
            while (duration < _multishotScriptableObject.Duration)
            {
                Use();
                duration += Time.deltaTime;
                _lastUsedTimer = Time.time;
                _canUseFirstTime = false;

                yield return null;
            }

            CooldownTime = _lastUsedTimer + _multishotScriptableObject.CooldownTime - Time.time;//потом сделать визуализацию кулдауна
        }
        else
        {
            Debug.Log("Осталось " + (_lastUsedTimer + _multishotScriptableObject.CooldownTime - Time.time));
        }
    }

    private void Use()
    {
        Vector3 forward = transform.forward;

        for (int i = 0; i < _numberOfArrows; i++)
        {
            float angle = (-_spreadAngle / 2) + (i * (_spreadAngle / (_numberOfArrows - 1)));
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Arrow arrow = _arrowSpawner.Spawn(transform, rotation * Quaternion.LookRotation(forward));
            arrow.StartFly(transform.forward, transform.position);
        }
    }
}
