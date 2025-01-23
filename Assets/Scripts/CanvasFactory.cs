using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "CanvasFactory", menuName = "CanvasFactory")]
public class CanvasFactory : ScriptableObject
{
    [SerializeField] private Canvas _meleeCanvas;
    [SerializeField] private Canvas _rangeCanvas;

    public Canvas Get(CharacterType characterType)
    {
        Canvas canvas = Instantiate(GetPrefab(characterType));

        return canvas;
    }

    private Canvas GetPrefab(CharacterType characterType)
    {
        switch (characterType)
        {
            case CharacterType.Melee:
                return _meleeCanvas;
            case CharacterType.Range:
                return _rangeCanvas;

            default:
                throw new ArgumentException(nameof(characterType));
        }
    }
}
