using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private bool isAlive = false;
    private int numNeighbours = 0;
    private GameController controller;
    private int minNeighbours;
    private int maxNeighbours;

    public void cellInit()
    {
        controller = FindObjectOfType<GameController>();
        GetComponent<SpriteRenderer>().color = controller.deadCellColor;
        minNeighbours = controller.minNeighbours;
        maxNeighbours = controller.maxNeighbours;
    }

    //Get the number of neighbours and change the current state
    public void setNeighbours(int num)
    {
        //Rules:
        //1.Any live cell with fewer than two live neighbours dies, as if by underpopulation.
        //2.Any live cell with two or three live neighbours lives on to the next generation.
        //3.Any live cell with more than three live neighbours dies, as if by overpopulation.
        //4.Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

        numNeighbours = num;
        if(isAlive)
        {
            if(num < minNeighbours || num > maxNeighbours)
            {
                setAlive(false);
            }
        }
        else
        {
            if(num > minNeighbours && num <= maxNeighbours)
            {
                setAlive(true);
            }
        }
    }

    //Change the live status and color
    public void setAlive(bool value)
    {
        isAlive = value;
        if (value)
            GetComponent<SpriteRenderer>().color = controller.aliveCellColor;
        else
            GetComponent<SpriteRenderer>().color = controller.deadCellColor;
    }

    //Get the live status of the cell
    public bool IsAlive()
    {
        return isAlive;
    }
}
