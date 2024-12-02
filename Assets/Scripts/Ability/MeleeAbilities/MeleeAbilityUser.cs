using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAbilityUser : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private BorrowedTimeUser _borrowedTime;
    [SerializeField] private BladeFuryUser _bladeFury;

    [Header("SO")]
    [SerializeField] private BorrowedTime _borrowedTimeScriptableObject;
    [SerializeField] private BladeFury _bladeFuryScriptableObject;

    public void UseFirstAblitity()
    {
        _bladeFury.Upgrade(_bladeFuryScriptableObject);
        StartCoroutine(_bladeFury.UseAbility());
    }

    public void UseSecondAbility()
    {
        _borrowedTime.Upgrade(_borrowedTimeScriptableObject);
        //_borrowedTime.UseAbility();
    }

    public void ActivateBorrowedTime()
    {
        _borrowedTime.IsWorkingTrue();
        _borrowedTime.UseAbility();
    }
}
