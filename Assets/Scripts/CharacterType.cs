using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "chatacter", fileName = "PlayerCharacter")]
public class CharacterType : ScriptableObject, ICharacteristicable
{
    [field: SerializeField] public int Power { get; private set; }

    [field: SerializeField] public int Armor { get; private set; }

    [field: SerializeField] public int MaxHealth { get; private set; }
}
