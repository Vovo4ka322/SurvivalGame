using UnityEngine;

[CreateAssetMenu(fileName = "Blur", menuName = "Ability/Range/Blur")]
public class Blur : Ability
{
    [field: SerializeField] public float Evasion {  get; private set; }   
}
