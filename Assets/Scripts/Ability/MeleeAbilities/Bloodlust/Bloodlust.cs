using UnityEngine;

[CreateAssetMenu(fileName = "Bloodlust", menuName = "Ability/Melee/Bloodlust")]
public class Bloodlust : Ability
{
    [field: SerializeField] public float MovementSpeed { get; private set; }
    
    [field: SerializeField] public float AttackSpeed { get; private set; }
}
