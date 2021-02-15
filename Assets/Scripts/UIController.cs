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
    public GameObject playButton;
    public GameObject helpButton;
    public GameObject mainMenu;
    public GameObject helpMenu;
    public GameObject settings;

    private GameController controller;
    public int width;
    public int height;
    public float step;

    void Start()
    {
        controller = GetComponent<GameController>();
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Help()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        helpMenu.SetActive(false);
    }

    public void Pause()
    {
        controller.isPlaying = false;
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
        quitButton.SetActive(true);
        settings.SetActive(true);
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
        controller.isPlaying = true;
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        settings.SetActive(false);
    }

    public void SetWidth(string width)
    {
        if(width != "")
            this.width = int.Parse(width);
    }

    public void SetHeight(string height)
    {
        if(height != "")
            this.height = int.Parse(height);
    }

    public void SetStep(string step)
    {
        if(step != "")
            this.step = int.Parse(step);
    }

    public void Generate()
    {
        controller.Generate(width, height, step);
    }
}
