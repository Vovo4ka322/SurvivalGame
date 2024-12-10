using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAbilityUser : MonoBehaviour//преобразовать этот класс в нормальный вид
{
    [SerializeField] private Player _player;
    [SerializeField] private BorrowedTimeUser _borrowedTime;
    [SerializeField] private BladeFuryUser _bladeFury;

    [Header("Base scriptable object")]
    [SerializeField] private BorrowedTime _borrowedTimeScriptableObject;
    [SerializeField] private BladeFury _bladeFuryScriptableObject;
    [SerializeField] private Bloodlust _bloodlustScriptableObject;

    [Header("scriptable object lvl2")]
    [SerializeField] private BorrowedTime _borrowedTimeScriptableObject2;
    [SerializeField] private BladeFury _bladeFuryScriptableObject2;
    [SerializeField] private Bloodlust _bloodlustScriptableObject2;

    [Header("scriptable object lvl3")]
    [SerializeField] private BorrowedTime _borrowedTimeScriptableObject3;
    [SerializeField] private BladeFury _bladeFuryScriptableObject3;
    [SerializeField] private Bloodlust _bloodlustScriptableObject3;

    public event Action LevelChanged;
    public event Action AbilityUpgraded;

    private void Awake()
    {
        _bladeFury.Upgrade(_bladeFuryScriptableObject);
        _borrowedTime.Upgrade(_borrowedTimeScriptableObject);
        _player.UpgradeCharacteristikByBloodlust(_bloodlustScriptableObject);
    }

    private void OnEnable()//хз где, но где-то тут должно вылезать окошко, в котором мы выбираем что улучшить
    {
        _player.Level.LevelChanged += OpenUpgraderWindow;
    }

    private void OnDisable()
    {
        _player.Level.LevelChanged -= OpenUpgraderWindow;
    }

    public void OpenUpgraderWindow()//тут проблема, окошко не запускается
    {
        LevelChanged?.Invoke();
        Debug.Log("Wokr");
    }

    public void UseFirstAblitity()
    {
        StartCoroutine(_bladeFury.UseAbility(_player.transform));
    }

    public void UseSecondAbility()
    {
        StartCoroutine(_borrowedTime.UseAbility(_player));
    }

    public void UseThirdAbility()
    {
        _player.UpgradeCharacteristikByBloodlust(_bloodlustScriptableObject);
    }

    public void UpgradeFirstAbility()
    {    
        _bladeFury.Upgrade(_bladeFuryScriptableObject2);
        AbilityUpgraded?.Invoke();
    }

    public void UpgradeSecondAbility()
    {
        _borrowedTime.Upgrade(_borrowedTimeScriptableObject2);
        AbilityUpgraded?.Invoke();
    }

    public void UpgradeThirdAbility()
    {
        _player.UpgradeCharacteristikByBloodlust(_bloodlustScriptableObject2);
        AbilityUpgraded?.Invoke();
    }
}
