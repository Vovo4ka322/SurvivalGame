using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Ability/Melee/AbilityData")]
public class AbilityData : ScriptableObject
{
    [field: SerializeField] public BorrowedTime BorrowedTimeScriptableObject {  get; private set; }
    [field: SerializeField] public BladeFury BladeFuryScriptableObject { get; private set; }
    [field: SerializeField] public Bloodlust BloodlustScriptableObject { get; private set; }
}
