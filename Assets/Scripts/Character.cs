using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [field:SerializeField] public Health Health {  get; private set; }
}
