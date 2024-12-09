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
    [SerializeField] private Bloodlust _bloodlustScriptableObject;

    public void UseFirstAblitity()
    {
        _bladeFury.Upgrade(_bladeFuryScriptableObject);
        StartCoroutine(_bladeFury.UseAbility(_player.transform));
    }

    public void UseSecondAbility()
    {
        _borrowedTime.Upgrade(_borrowedTimeScriptableObject);
        StartCoroutine(_borrowedTime.UseAbility(_player));
    }

    public void UseThirdAbility()
    {
        _player.UpgradeCharacteristikByBloodlust(_bloodlustScriptableObject);
    }
}
