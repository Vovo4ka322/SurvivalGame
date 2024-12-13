using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Ability/Melee/AbilityData")]
public class AbilityData : SerializedScriptableObject
{
    [field: SerializeField] public BorrowedTime _borrowedTimeScriptableObject {  get; private set; }
    [field: SerializeField] public BladeFury _bladeFuryScriptableObject { get; private set; }
    [field: SerializeField] public Bloodlust _bloodlustScriptableObject { get; private set; }
}
