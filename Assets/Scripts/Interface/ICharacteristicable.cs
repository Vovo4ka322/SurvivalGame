using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacteristicable
{
    public int Power { get; }

    public int Armor { get; }

    public int MaxHealth { get; }
}
