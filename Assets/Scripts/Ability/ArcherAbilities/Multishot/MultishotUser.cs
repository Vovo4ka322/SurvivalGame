using MainPlayer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotUser : MonoBehaviour, ICooldownable
{
    [SerializeField] private ArrowSpawner _arrowSpawner;
    [SerializeField] private Bow _bow;
    [SerializeField] private Arrow _arrow;

    private Multishot _multishotScriptableObject;
    private float _lastUsedTimer = 0;
    private bool _canUseFirstTime = true;
    private int _numberOfArrows = 5;
    private float _spreadAngle = 60f;
    private bool _isWorking = false;

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
                _isWorking = true;

                while (_isWorking)//переделать, работает неккоректно. Нужно сделать небольшую задержку между выстрелов, а также отключить обычную атаку во время использования способности
                {
                    CalculateArrowFlight();

                    yield return new WaitForSeconds(0.5f);
                }

                duration += Time.deltaTime;
                _lastUsedTimer = Time.time;
                _canUseFirstTime = false;

                yield return null;
            }

            _isWorking = false;
            CooldownTime = _lastUsedTimer + _multishotScriptableObject.CooldownTime - Time.time;//потом сделать визуализацию кулдауна
        }
        else
        {
            Debug.Log("Осталось " + (_lastUsedTimer + _multishotScriptableObject.CooldownTime - Time.time));
        }
    }

    public void CalculateArrowFlight()
    {
        float facingRotation = Mathf.Atan2(_bow.transform.position.y, _bow.transform.position.x) * Mathf.Rad2Deg;
        float startRotation = facingRotation + _spreadAngle / 2;
        float angleIncrease = _spreadAngle / ((float)_numberOfArrows - 1);

        for (int i = 0; i < _numberOfArrows; i++)
        {
            float tempRotation = startRotation - angleIncrease * i;
            Arrow arrow = _arrowSpawner.Spawn(transform, Quaternion.Euler(0, 0, tempRotation));
            arrow.StartFly(Quaternion.Euler(0, tempRotation, 0) * _bow.transform.forward, _bow.transform.position);
        }
    }

    private IEnumerator Fly()
    {
        while (_isWorking)
        {
            CalculateArrowFlight();

            yield return new WaitForSeconds(0.5f);
        }
    }
}
