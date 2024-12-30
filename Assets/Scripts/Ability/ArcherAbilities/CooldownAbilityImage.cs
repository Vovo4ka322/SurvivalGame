using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CooldownImage", menuName = "Ability/Image")]
public class CooldownAbilityImage : ScriptableObject
{
    [field: SerializeField] public Image FirstImage { get; private set; }

    [field:SerializeField] public Image SecondImage {  get; private set; }

    //private Dictionary<int, Image> _cooldownImages;

    public int FirstAbility => 0;

    public int SecondAbility => 1;

    
    //public Image FirstImage => _firstCooldownImage;

    //public Image SecondImage => _secondCooldownImage;

    //public IReadOnlyDictionary<int, Image> CooldownImages => _cooldownImages;

    //public void Init()
    //{
    //    _cooldownImages = new Dictionary<int, Image>
    //    {
    //        {FirstAbility, _firstCooldownImage},
    //        {SecondAbility, _secondCooldownImage}
    //    };

    //    //_cooldownImages = new Dictionary<int, Image>();

    //    //_cooldownImages.Add(FirstAbility, _firstCooldownImage);
    //    //_cooldownImages.Add(SecondAbility, _secondCooldownImage);
    //}
}