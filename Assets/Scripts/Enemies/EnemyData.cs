using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "EnemyType")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public EnemyType EnemyType {  get; private set; }

    [field: SerializeField] public float MoveSpeed {  get; private set; }

    [field: SerializeField] public float Damage { get; private set; }

    [field: SerializeField] public float MaxHealth { get; private set; }

    [field: SerializeField] public int Experience { get; private set; }

    [field: SerializeField] public int Gold { get; private set; }
}