using Ability.ArcherAbilities;
using Ability.MeleeAbilities;
using PlayerComponents;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CanvasFactory", menuName = "CanvasFactory")]
public class CanvasFactory : ScriptableObject
{
    [SerializeField] private Canvas _meleeCanvas;
    [SerializeField] private Canvas _rangeCanvas;

    public Canvas Create(CharacterType characterType, Player player)
    {
        Canvas canvas = Instantiate(GetPrefab(characterType));

        canvas.GetComponent<PlayerHealthAndExperienceViewer>().Init(player);

        if (characterType == CharacterType.Melee)
        {
            canvas.GetComponent<MeleeWindowImprovment>().Init(player);
            canvas.GetComponent<MeleeAbilityViewer>().Init(player);
            canvas.GetComponent<MeleeCanvasInitialization>().InitButtons(player);
        }
        else if (characterType == CharacterType.Range)
        {
            canvas.GetComponent<RangeWindowImprovment>().Init(player);
            canvas.GetComponent<RangeAbilityViewer>().Init(player);
            canvas.GetComponent<RangeCanvasInitialization>().InitButtons(player);
        }

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
