using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    [SerializeField] private GameObject pauseWindow;

    [SerializeField] public GameObject gameplayUI;

    public bool isPaused = false;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.instance.playerLost)
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0f;
                pauseWindow.SetActive(true);
                gameplayUI.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f;
                gameplayUI.SetActive(true);
                pauseWindow.SetActive(false);
            }
      
        }
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        gameplayUI.SetActive(true);
        pauseWindow.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        gameplayUI.SetActive(false);
    }
}
