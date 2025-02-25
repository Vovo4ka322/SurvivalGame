using UnityEngine;

[CreateAssetMenu(fileName = "Personage", menuName = "Personage")]
public class Personage : ScriptableObject, IPersonageble
{
    [field: SerializeField] public float Health { get; private set; }

    [field: SerializeField] public float Armor { get; private set; }

    [field: SerializeField] public float Damage {  get; private set; }

    [field: SerializeField] public float AttackSpeed { get; private set; }

    [field: SerializeField] public float MovementSpeed { get; private set; }
}