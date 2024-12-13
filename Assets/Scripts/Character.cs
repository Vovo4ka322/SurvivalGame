using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [field:SerializeField] public Health Health {  get; private set; }
}
