using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Button continum;
    [SerializeField] private Button mainMenu;
    [SerializeField] private Button exit;

    private bool gameIsPaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Pause ()
    {
        mainPanel.SetActive(true);
        Time.timeScale = 0.001f;
        gameIsPaused = true;
        continum.Select();
    }
    public void Continue ()
    {
        mainPanel.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void MainMenu()
    {
        SoundManager.play.StopPlaySound("Any");
        mainPanel.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
