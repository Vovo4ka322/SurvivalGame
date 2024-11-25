using UnityEngine;

public interface IAbilitable
{
    public string Name { get; }

    public string Description { get; }

    public float CooldownTime {  get; }
}
