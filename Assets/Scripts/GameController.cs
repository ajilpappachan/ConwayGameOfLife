using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Grid Setup")]
    [SerializeField] private int width = 6;
    [SerializeField] private int height = 6;
    [SerializeField] private GameObject cellObject;
    [SerializeField] private GameObject gridParent;
    private float stepDuration = 0.1f;
    private Cell[,] grid;
    private float timer;

    [Header("Cell Properties")]
    public Color deadCellColor;
    public Color aliveCellColor;

    [Header("Game State")]
    public bool isPlaying = false;
    private UIController ui;

    // Start is called before the first frame update
    void Start()
    {
        grid = new Cell[width, height];
        gridInit();
        randomPattern();
        setUpUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying)
        {
            if (timer > stepDuration)
            {
                timer = 0;
                updateCells();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

    }

    private void setUpUI()
    {
        ui = GetComponent<UIController>();
        ui.height = height;
        ui.width = width;
        ui.step = stepDuration;
    }

    public void Generate()
    {
        gridDestroy();
        grid = new Cell[width, height];
        gridInit();
        randomPattern();
    }

    public void Generate(int width, int height, float step)
    {
        gridDestroy();
        this.height = height;
        this.width = width;
        stepDuration = step;
        grid = new Cell[width, height];
        gridInit();
        randomPattern();
    }

    private void gridInit()
    {
        //Create Cells
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                GameObject cell = Instantiate(cellObject, new Vector2(x, y), Quaternion.identity);
                cell.GetComponent<Cell>().cellInit();
                cell.transform.SetParent(gridParent.transform);
                grid[x, y] = cell.GetComponent<Cell>();
            }
        }

        //Set Camera to Center of the Grid and fit the grid inside view
        Camera.main.transform.position = new Vector3(width / 2, height / 2, -10f);
        if (width == height)
            Camera.main.orthographicSize = width / 2;
        else
            Camera.main.orthographicSize = width > height ? width / 2 : height / 2;
    }

    private void gridDestroy()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Destroy(grid[x, y].gameObject);
            }
        }

    }

    private void updateCells()
    {
        //Rules:
        //1.Any live cell with fewer than two live neighbours dies, as if by underpopulation.
        //2.Any live cell with two or three live neighbours lives on to the next generation.
        //3.Any live cell with more than three live neighbours dies, as if by overpopulation.
        //4.Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int liveNeighbours = checkNeighbours(x, y);
                grid[x, y].setNeighbours(liveNeighbours);
            }
        }
    }

    //Get number of live neighbours
    private int checkNeighbours(int x, int y)
    {
        int liveNeighbours = 0;
        if (x > 0)
            liveNeighbours += grid[x - 1, y].IsAlive() ? 1 : 0;
        if (x < width - 1)
            liveNeighbours += grid[x + 1, y].IsAlive() ? 1 : 0;
        if (y > 0)
            liveNeighbours += grid[x, y - 1].IsAlive() ? 1 : 0;
        if (y < height - 1)
            liveNeighbours += grid[x, y + 1].IsAlive() ? 1 : 0;
        if (x > 0 && y > 0)
            liveNeighbours += grid[x - 1, y - 1].IsAlive() ? 1 : 0;
        if (x < width - 1 && y < height - 1)
            liveNeighbours += grid[x + 1, y + 1].IsAlive() ? 1 : 0;
        if (x > 0 && y < height - 1)
            liveNeighbours += grid[x - 1, y + 1].IsAlive() ? 1 : 0;
        if (x < width - 1 && y > 0)
            liveNeighbours += grid[x + 1, y - 1].IsAlive() ? 1 : 0;
        return liveNeighbours;
    }

    private void randomPattern()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int randomValue = Mathf.RoundToInt(Random.Range(0.0f, 1.0f));
                if (randomValue == 1)
                    grid[x, y].setAlive(true);
                else
                    grid[x, y].setAlive(false);
            }
        }
    }

}
