using PlayerComponents;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MenuComponents
{
    public class Defeat : MonoBehaviour
    {
        [SerializeField] private Image _deathScreenPanel;

        private GameplayMenu _menu;
        private Player _player;

        private void OnEnable()
        {
            _player.Death += TurnOn;
        }

        private void OnDisable()
        {
            _player.Death -= TurnOn;
        }

        public void Init(Player player)
        {
            _player = player;
        }

        private void TurnOn()
        {
            _deathScreenPanel.gameObject.SetActive(true);
        }
    }
}