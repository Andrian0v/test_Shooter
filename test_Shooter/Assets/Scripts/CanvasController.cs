using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _crosshair;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private GameObject _pausePanel;

    void Start()
    {
        _crosshair.SetActive(true);
        _gameOverPanel.SetActive(false);
        CursorState(false);
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            GameOver(0);
        }
        if ((_playerCharacter.PlayerHealthGet() <= 0))
        {
            GameOver(1);
        }
    }
    private void GameOver(int value)
    {
        switch (value)
        {
            case 0:
                Time.timeScale = 0f;
                CursorState(true);
                _crosshair.SetActive(false);
                _pausePanel.SetActive(true);
                break;
            case 1:
                Time.timeScale = 0f;
                CursorState(true);
                _crosshair.SetActive(false);
                _gameOverPanel.SetActive(true);
                break;
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    void CursorState(bool state)
    {
        if (state)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
        _crosshair.SetActive(true);
        CursorState(false);
    }
}
