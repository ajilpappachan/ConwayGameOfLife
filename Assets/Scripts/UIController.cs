using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject quitButton;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void Quit()
    {
        //If main menu, quit the game. Otherwise go to main menu
        if (SceneManager.GetActiveScene().buildIndex == 0)
            Application.Quit();
        else
            SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
    }
}
