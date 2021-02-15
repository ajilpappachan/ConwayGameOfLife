using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private bool isAlive = false;
    private int numNeighbours = 0;
    private GameController controller;

    public void cellInit()
    {
        controller = FindObjectOfType<GameController>();
        GetComponent<SpriteRenderer>().color = controller.deadCellColor;
    }

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
            if(num < 2 || num > 3)
            {
                setAlive(false);
            }
        }
        else
        {
            if(num == 3)
            {
                setAlive(true);
            }
        }
    }

    public void setAlive(bool value)
    {
        isAlive = value;
        if (value)
            GetComponent<SpriteRenderer>().color = controller.aliveCellColor;
        else
            GetComponent<SpriteRenderer>().color = controller.deadCellColor;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}
