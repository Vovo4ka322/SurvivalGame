using HealthComponents;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class Character : MonoBehaviour
{
    public Health Health {  get; private set; }
}