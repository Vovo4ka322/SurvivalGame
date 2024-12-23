using UnityEngine;

[CreateAssetMenu(fileName = "Blur", menuName = "Ability/Range/Blur")]
public class Blur : Ability
{
    [field: SerializeField] public int Evasion {  get; private set; }   
}
