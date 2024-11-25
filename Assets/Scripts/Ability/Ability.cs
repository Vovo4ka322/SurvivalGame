using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject, IAbilitable
{
    [field: SerializeField] public string Name { get; private set; }

    [field: SerializeField] public string Description { get; private set; }

    [field: SerializeField] public float CooldownTime { get; private set; }
}
