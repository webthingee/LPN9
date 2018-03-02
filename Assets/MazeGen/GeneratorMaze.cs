using System.Collections; // https://forum.unity.com/threads/quick-maze-generator.173370/
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityEditor;

public class Cell
{
    public bool isVisited;
    public GameObject north;
    public GameObject south;
    public GameObject east;
    public GameObject west;
    public GameObject floor;
    public int current; //Debug
    public Vector3 cellPosition; //Debug
}

[System.Serializable]
public class MazeUnit
{
    public int[,] mazeUnitPosition;
    public Vector2Int zPoint;
    public string code = "";
    public int mazeUnitCode = 0;
}

public class GeneratorMaze : MonoBehaviour
{
    [Header("Settings")]
    [Range(3, 200)] public int xSize;//size axis X
    [Range(3, 200)] public int ySize;//size axis Y
    [Range(1, 50)] public int sizeWall; //Heigth and Width Wall
    public GameObject wall; //GameObject Wall
    public GameObject floor;//GameObject Floor

    [Header("Read-Only Properties")]
    private GameObject Floor; //Create GameObject Floor
    private Vector3 initialPos; //start position Maze
    private GameObject[] walls;
    private GameObject[] floors;
    private float middle;
    private GameObject Maze;
    private List<Cell> cells;
    private int eastLimited;
    private int southLimited;
    private int northLimited;
    private int westLimited;
    private int currentRow;
    private List<Cell> lastCellVisited;
    private int indexCell;
    //private int currentI;
    private Cell currentCell;
    private List<Cell> neighboringCell;
    private GameObject parent;
    private int range;

    // Maze Unit Code Generation
    private List<GameObject> ListOfWalls = new List<GameObject>();
    public List<MazeUnit> ListOfMazeUnits = new List<MazeUnit>();
    public bool hasListOfMazeUnits = false;

    void Start()
    {
        Create();
    }

    void Create()
    {
        //CELL INIT
        middle = (float)sizeWall / 2;
        initialPos = Vector3.zero;
        this.transform.position = initialPos;
        lastCellVisited = new List<Cell>();
        neighboringCell = new List<Cell>();
        cells = new List<Cell>();

        //currentI = 0;
        range = sizeWall;

        createWalls();
        createFloor();
        createCell();

        GameMaster.GM.mazeBaseCreated = true;
    }

    void createFloor()
    {
        Floor = new GameObject("Floor"); //group all the Quad to keep everything in order
		Floor.transform.parent = this.transform;
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                GameObject insFloor = Instantiate(floor, new Vector3((j * sizeWall) + middle, i * sizeWall, middle), Quaternion.Euler(0, 0, 0)) as GameObject;
                insFloor.transform.localScale = new Vector3(sizeWall, sizeWall, 0.1f);
                insFloor.transform.parent = Floor.transform; //group all the Quad to keep everything in order
                indexCell = (i * xSize) + j; //I transform the array into an array
                insFloor.transform.name = i.ToString() + " - " + j.ToString();
                insFloor.GetComponent<Floor>().posX = i;
                insFloor.GetComponent<Floor>().posY = j;
            }
        }
    }

    void createWalls()
    {
        Maze = new GameObject("Maze Walls"); //group all the Quad to keep everything in order
        Maze.transform.parent = this.transform;
        //Y
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                GameObject wallY = Instantiate(wall, new Vector3(initialPos.x + (j * sizeWall), initialPos.z + (i * sizeWall), 0), Quaternion.identity) as GameObject;
                wallY.name = i + "y " + j + "" + i;
                ListOfWalls.Add(wallY);
                wallY.transform.localScale = new Vector3(0.1f, sizeWall, sizeWall);
                wallY.transform.parent = Maze.transform;
            }
        }

        //X
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                GameObject wallX = Instantiate(wall, new Vector3(initialPos.x + (j * sizeWall) + middle, initialPos.z + (i * sizeWall) - middle, 0), Quaternion.Euler(0, 90, 90)) as GameObject;
                wallX.name = i + "x " + j + "" + i;
                ListOfWalls.Add(wallX);
                wallX.transform.localScale = new Vector3(0.1f, sizeWall, sizeWall);
                wallX.transform.parent = Maze.transform;
            }
        }
    }

    void CreateMazeUnit ()
    {
        for (int i = 0; i <= xSize - 1; i++) // TODO: Why?
        {
            for (int j = 0; j < ySize; j++)
            {
                MazeUnit mazeUnit = new MazeUnit();
                mazeUnit.mazeUnitPosition = new int[i,j];
                mazeUnit.zPoint = new Vector2Int(i,j);
                foreach (var item in ListOfWalls) // TODO: get rid of the strings, use better variables
                {
                    string yAxis = "y";
                    string west = i.ToString() + j.ToString();
                    string east = (1 + i).ToString() + j.ToString();
                    string xAxis = "x";
                    string south = i.ToString() + j.ToString();
                    string north = i.ToString() + (1 + j).ToString();
                    
                    if (item.name.Contains(xAxis) && item.name.Contains(north))
                    {
                        mazeUnit.code += "N";
                        mazeUnit.mazeUnitCode += 11000; // TODO: Depricated? not sure I need these now?
                    }
                    
                    if (item.name.Contains(yAxis) && item.name.Contains(east))
                    {
                        mazeUnit.code += "E";
                        mazeUnit.mazeUnitCode += 10100;
                    }
                    
                    if (item.name.Contains(xAxis) && item.name.Contains(south))
                    {
                        mazeUnit.code += "S";
                        mazeUnit.mazeUnitCode += 10010;
                    }

                    if (item.name.Contains(yAxis) && item.name.Contains(west))
                    {
                        mazeUnit.code += "W";
                        mazeUnit.mazeUnitCode += 10001;
                    }
                }
                ListOfMazeUnits.Add(mazeUnit);
            }
        }
        hasListOfMazeUnits = true;
    }

    void createCell()
    {
        walls = new GameObject[Maze.transform.childCount];
        floors = new GameObject[xSize * ySize];

        int south = (xSize + 1) * ySize;
        int north = ((xSize + 1) * ySize) + xSize;

        for (int i = 0; i < Maze.transform.childCount; i++)
        {
            walls[i] = Maze.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < xSize * ySize; i++)
        {
            floors[i] = Floor.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < xSize * ySize; i++)
        {

            cells.Add(new Cell());
            currentRow = (i / xSize); //find current row for every cell

            cells[i].south = walls[i + south];
            cells[i].north = walls[i + north];
            cells[i].east = walls[(currentRow + i) + 1];
            cells[i].west = walls[currentRow + i];
            cells[i].floor = floors[i];
            cells[i].current = i;
        }

        mazeCreation(cells);
    }

    void AddNears(Cell currentCell)
    {
        int currentRow = (currentCell.current / xSize) + 1;
        int eastLimited = currentCell.current % xSize;
        int westLimited = eastLimited;
        int southLimited = xSize - 1;
        int northLimited = currentCell.current / xSize;

        //East
        if (eastLimited != xSize - 1)
        {
            if (cells[currentCell.current + 1].isVisited == false)
                neighboringCell.Add(cells[currentCell.current + 1]);

        }
        //West
        if (westLimited != 0)
        {
            if (cells[currentCell.current - 1].isVisited == false)
                neighboringCell.Add(cells[currentCell.current - 1]);

        }
        //South
        if (currentCell.current > southLimited)
        {
            if (cells[currentCell.current - xSize].isVisited == false)
                neighboringCell.Add(cells[currentCell.current - xSize]);

        }
        //North
        if (northLimited != ySize - 1)
        {
            if (cells[currentCell.current + xSize].isVisited == false)
                neighboringCell.Add(cells[currentCell.current + xSize]);

        }
    }

    void breakWall(Cell curr, Cell choose)
    {
        //East
        if (choose.current == curr.current + 1)
        {
            Destroy(curr.east);
            ListOfWalls.Remove(curr.east);
            currentCell = cells[currentCell.current + 1];
        }
        //West
        if (choose.current == curr.current - 1)
        {
            Destroy(curr.west);
            ListOfWalls.Remove(curr.west);
            currentCell = cells[currentCell.current - 1];
        }
        //South
        if (choose.current == curr.current - xSize)
        {
            Destroy(curr.south);
            ListOfWalls.Remove(curr.south);
            currentCell = cells[currentCell.current - xSize];
        }
        //North
        if (choose.current == curr.current + xSize)
        {
            Destroy(curr.north);
            ListOfWalls.Remove(curr.north);
            currentCell = cells[currentCell.current + xSize];
        }
    }

    void mazeCreation(List<Cell> allCells)
    {
        int visitedCell = 0;

        int random = Random.Range(0, allCells.Count); //I choose a cell at random
        currentCell = allCells[random]; //the choice cell becomes the current cell

        while (visitedCell < allCells.Count) //I run the cycle for all the cells of the matrix
        {
            AddNears(currentCell); //add to the selection cell neighboring cells
            int randomNeighboring = Random.Range(0, neighboringCell.Count); //I choose a neighboring cell at random

            if (neighboringCell.Any()) //if there are cells neighboring the current cell
            {
                lastCellVisited.Add(currentCell); //becomes visited

                breakWall(currentCell, neighboringCell[randomNeighboring]); //Break the wall

                neighboringCell.Clear(); //Reset the list of neighboring cells
                visitedCell++;
                currentCell.isVisited = true;
            }

            else
            {
                //else I choose a already been visited cell
                random = Random.Range(0, lastCellVisited.Count);
                currentCell = lastCellVisited[random];
            }
        }

        CreateMazeUnit();
    }
}