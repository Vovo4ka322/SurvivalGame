using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InsatiableHunger", menuName = "Ability/Range/InsatiableHunger")]
public class InsatiableHunger : Ability, ICooldownable, IDamageCausable
{
    [field: SerializeField] public float CooldownTime {  get; private set; }

    [field: SerializeField] public float Damage { get; private set; }

    [field: SerializeField] public float Vampirism { get; private set; }
}
