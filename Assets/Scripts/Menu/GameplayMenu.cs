using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameplayMenu : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _continuationButton;
    [SerializeField] private Image _pausePanel;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(Pause);
        _menuButton.onClick.AddListener(ExitToMenu);
        _continuationButton.onClick.AddListener(ContinueGame);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(Pause);
        _menuButton.onClick.RemoveListener(ExitToMenu);
        _continuationButton.onClick.RemoveListener(ContinueGame);
    }

    private void Pause()
    {
        Time.timeScale = 0;
        _pausePanel.gameObject.SetActive(true);
    }

    private void ExitToMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    private void ContinueGame()
    {
        Time.timeScale = 1.0f;
        _pausePanel.gameObject.SetActive(false);
    }
}
