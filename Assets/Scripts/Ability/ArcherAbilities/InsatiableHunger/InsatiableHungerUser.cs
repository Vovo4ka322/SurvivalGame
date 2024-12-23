using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InsatiableHungerUser : MonoBehaviour
{
    private Multishot _insatiableHunger;
    private float _lastUsedTimer = 0;
    private bool _canUseFirstTime = true;

    public float CooldownTime { get; private set; }

    public void Upgrade(Multishot multishot)
    {
        _insatiableHunger = multishot;
    }

    public IEnumerator UseAbility(IActivable activable, IVampirismable vampirismable)
    {
        Debug.Log(_insatiableHunger.CooldownTime + " Cooldown");
        float duration = 0;

        if (Time.time >= _lastUsedTimer + _insatiableHunger.CooldownTime || _canUseFirstTime)
        {
            while (duration < _insatiableHunger.Duration)
            {
                activable.SetState(true);
                vampirismable.SetVampirismState(true);
                duration += Time.deltaTime;
                _lastUsedTimer = Time.time;
                _canUseFirstTime = false;

                yield return null;
            }

            vampirismable.SetVampirismState(false);
            activable.SetState(false);

            CooldownTime = _lastUsedTimer + _insatiableHunger.CooldownTime - Time.time;//потом сделать визуализацию кулдауна
        }
        else
        {
            Debug.Log("Осталось " + (_lastUsedTimer + _insatiableHunger.CooldownTime - Time.time));
        }
    }
}
