using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridPlacement : MonoBehaviour 
{
	public GeneratorMaze maze;
	public GameObject room;
	public GameObject[] startingRoomPrefabs; // 0 is start platform, other stuff that goes in follow
    public GameObject[] endingRoomPrefabs;
    public GridID[] gridPrefabs;

	int numberOfTilesWidth = 20;
	int numberOfTilesHeight = 20;

    private List<GameObject> roomList = new List<GameObject>();

    void Start()
    {
        StartCoroutine(CreateLayout(0.1f));
    }

    IEnumerator CreateLayout(float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);

		if (!maze.hasListOfMazeUnits)
        {
            StartCoroutine(CreateLayout(0.1f));
        }
		else
		{
			LayoutGrid();
		}
    }

    void Update()
	{
		// Prevent Infinate Loops and Crashing
        var fps = 1.0f / Time.deltaTime;
     	if(fps < 1) {
        	Debug.Break();
     	}
	}

	void LayoutGrid()
	{		
		numberOfTilesWidth = room.GetComponent<RoomControl>().wallXPos * 2; 
		numberOfTilesHeight = room.GetComponent<RoomControl>().wallYPos * 2; 

        if (maze.hasListOfMazeUnits)
		{
			foreach (MazeUnit unit in maze.ListOfMazeUnits)
			{
				Vector2Int posAdjust = new Vector2Int (numberOfTilesWidth, numberOfTilesHeight);
				Vector2 pos = unit.zPoint * posAdjust;
				GameObject theRoom = Instantiate(room, pos, Quaternion.identity, this.transform);
                theRoom.name = unit.zPoint.ToString();
                theRoom.GetComponent<RoomControl>().placementX = unit.zPoint.x;
                theRoom.GetComponent<RoomControl>().placementY = unit.zPoint.y;
				theRoom.GetComponent<GridID>().passedGridID = unit.mazeUnitCode;
				theRoom.GetComponent<GridID>().mazeUnitCode = unit.code;

                roomList.Add(theRoom);
				// foreach (GridID grid in gridPrefabs)
				// {
				// 	if (unit.mazeUnitCode == grid.gridID)
				// 	{
				// 		Vector2Int posAdjust = new Vector2Int (numberOfTilesWidth, numberOfTilesHeight);
				// 		Vector2 pos = unit.zPoint * posAdjust;
				// 		Instantiate(grid, pos, Quaternion.identity);
				// 	}
				// }
			}
		}
		GameObject.Find("Maze").SetActive(false);
        GameMaster.GM.mazeGridCreated = true;
        GameMaster.GM.startingRoom = PickStartingRoom();
        GameMaster.GM.endingRoom = PickEndingRoom();
	}

    GameObject PickStartingRoom ()
    {
        GameObject startingRoom = null;

        List<GameObject> startRoomOptions = new List<GameObject>();

        foreach (GameObject room in roomList)
        {
            if (room.GetComponent<RoomControl>().placementY == 3)
            {
                if (room.GetComponent<GridID>().mazeUnitCode.Contains("S"))
                {
                    startRoomOptions.Add(room);
                }
            }
        }
        
        if (startRoomOptions.Count > 0)
        {
            int rand = Random.Range(0, startRoomOptions.Count);
            startingRoom = startRoomOptions[rand];
        }
        else
        {
            Debug.Log("no starting point, using first room");
            startingRoom = roomList[3]; // 0 = bottom 3 = top
        }
        startingRoom.GetComponent<RoomControl>().isStartingRoom = true;            
        //Instantiate(startingRoomPrefabs[0], startingRoom.transform.position, Quaternion.identity, startingRoom.transform);
        //startingRoom.GetComponent<RoomControl>().startingRoom = startingRoom;
        GameMaster.GM.mazeStartCreated = true;            

        return startingRoom;
    }

    GameObject PickEndingRoom ()
    {
        GameObject endingRoom = null;

        List<GameObject> endingRoomOptions = new List<GameObject>();

        foreach (GameObject room in roomList)
        {
            if (room.GetComponent<RoomControl>().placementY == 0)
            {
                endingRoomOptions.Add(room);
            }
        }
        
        int rand = Random.Range(0, endingRoomOptions.Count);
        endingRoom = endingRoomOptions[rand];
        endingRoom.GetComponent<RoomControl>().isEndingRoom = true;   
        //endingRoom.GetComponent<RoomControl>().endingRoom = endingRoom;
        //Instantiate(endingRoomPrefabs[0], endingRoom.transform.position, Quaternion.identity, endingRoom.transform);
        
        GameMaster.GM.mazeEndCreated = true;            

        return endingRoom;
    }

    public void AddRooms ()
    {
        foreach (GameObject room in roomList)
        {
            room.GetComponent<RoomControl>().RoomSelection(true);
        }
    }    

}
