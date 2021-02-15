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
    public int minNeighbours;
    public int maxNeighbours;

    void Start()
    {
        controller = GetComponent<GameController>();
    }

    //Play a new simulation
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    //Get help
    public void Help()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    //Go back from Help menu
    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        helpMenu.SetActive(false);
    }

    //Pause the current simulation
    public void Pause()
    {
        controller.isPlaying = false;
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
        quitButton.SetActive(true);
        settings.SetActive(true);
    }

    //Quit the game or simulation
    public void Quit()
    {
        //If main menu, quit the game. Otherwise go to main menu
        if (SceneManager.GetActiveScene().buildIndex == 0)
            Application.Quit();
        else
            SceneManager.LoadScene(0);
    }

    //Resume current simulation
    public void Resume()
    {
        controller.isPlaying = true;
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        settings.SetActive(false);
    }

    //Set the width property of the grid
    public void SetWidth(string width)
    {
        if(width != "")
            this.width = int.Parse(width);
    }

    //Set the height property of the grid
    public void SetHeight(string height)
    {
        if(height != "")
            this.height = int.Parse(height);
    }

    //Set the step duration property of the grid
    public void SetStep(string step)
    {
        if(step != "")
            this.step = int.Parse(step);
    }

    //Set the minimum neighbours property of the grid
    public void SetminNeighbours(string minNeighbours)
    {
        if (minNeighbours != "")
            this.minNeighbours = int.Parse(minNeighbours);
    }

    //Set the maximum neighbours property of the grid
    public void SetmaxNeighbours(string maxNeighbours)
    {
        if (maxNeighbours != "")
            this.step = int.Parse(maxNeighbours);
    }

    //Generate new grid based on the UI settings
    public void Generate()
    {
        controller.Generate(width, height, step, minNeighbours, maxNeighbours);
    }
}
