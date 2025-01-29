using PlayerComponents;
using UnityEngine;

public class CanvasInitialization : MonoBehaviour
{
    [SerializeField] private Canvas _meleeCanvas;
    [SerializeField] private Canvas _rangeCanvas;

    [SerializeField] private PlayerExperienceViewer _playerExperienceViewer;
    [SerializeField] private PlayerHealthViewer _playerHealthViewer;

    [field: SerializeField] public CharacterType CharacterType {  get; private set; }

    public void Init(Player player)
    {
        _playerExperienceViewer.Init(player);
        _playerHealthViewer.Init(player);
    }
}
