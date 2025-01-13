using UnityEngine;

public class Skin : MonoBehaviour
{
    [field: SerializeField] public Personage Personage {  get; private set; }

    [field: SerializeField] public PlayerSkins PlayerSkin { get; private set; }
}