using Ability.ArcherAbilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeAbilityViewer : MonoBehaviour
{
    [SerializeField] private ArcherAbilityUser _archerAbilityUser;
    [SerializeField] private CooldownAbilityImage _cooldownAbilityImage;
    [SerializeField] private Image _image;

    private Dictionary<int, Image> _cooldownImages;

    private void Awake()
    {
        _cooldownImages = new Dictionary<int, Image>
        {
            { _cooldownAbilityImage.FirstAbility, _cooldownAbilityImage.FirstImage },
            { _cooldownAbilityImage.SecondAbility, _cooldownAbilityImage.SecondImage },
        };

        //_cooldownAbilityImage.Init();
    }

    private void OnEnable()
    {
        _archerAbilityUser.MultishotUser.Used += OnMultishotChanged;
    }

    private void OnDisable()
    {
        _archerAbilityUser.MultishotUser.Used -= OnMultishotChanged;
    }

    private void OnMultishotChanged(float value)
    {
        //_cooldownAbilityImage.CooldownImages.TryGetValue(_cooldownAbilityImage.FirstAbility, out Image image);
        //if (_cooldownImages.TryGetValue(_cooldownAbilityImage.FirstAbility, out Image image))
        //    image.fillAmount = Mathf.InverseLerp(0, _archerAbilityUser.MultishotUser.Multishot.CooldownTime, value);
        //_cooldownAbilityImage.FirstElement().fillAmount = Mathf.InverseLerp(0, _archerAbilityUser.MultishotUser.Multishot.CooldownTime, value);
        _image.fillAmount = Mathf.InverseLerp(0, _archerAbilityUser.MultishotUser.Multishot.CooldownTime, value);

        //Debug.Log(_cooldownImages.Count + " _cooldownImages.Count");
        //_cooldownImages[_cooldownAbilityImage.FirstAbility].fillAmount = Mathf.InverseLerp(0, _archerAbilityUser.MultishotUser.Multishot.CooldownTime, value);
    }

    private void OnInsatiableHungerChanged(float value)
    {

    }
}
