using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomControl : MonoBehaviour 
{
	//public RoomStuff roomStuff;
    [Header("Tilemap Settings")]
    public Tilemap walkableTilemap;
    public Tilemap roomTilemap;
    public int wallXPos = 10;
    public int wallYPos = 10;
    
    [Header("Room Settings")]
    public int placementX;
    public int placementY;
    public bool isStartingRoom;
    public bool isEndingRoom;
    public bool isOnCompletionPath;

    [Header("Room Settings")]
    public bool addRooms;
    public bool testmode;
    public RoomPrefabs roomPrefabs;
    public List<Tile> TileList = new List<Tile>();

	private List<Vector2> LibWalkableTileVectors = new List<Vector2>();
	private List<TileBase> LibUnWalkableTileBases = new List<TileBase>();
    private GridID gridID;
	BoundsInt bounds;
	TileBase[] allTiles;

	void Start ()
    {
        bounds = walkableTilemap.cellBounds;
        allTiles = walkableTilemap.GetTilesBlock(bounds);
        gridID = GetComponent<GridID>();

        WallMaker();

        if (testmode)
        {
            RoomSelection(true);
        }
    }

    public void SetupOnPath ()
    {
        isOnCompletionPath = true;
        
    }

    public void RoomSelection(bool _allowRotation)
    {
        List<GameObject> chooseFrom = RoomCategoryOptions();
        
        int rand = Random.Range(0, chooseFrom.Count);
        
        int rotRand = Random.Range(0, 101);
        Quaternion rot = Quaternion.identity;
        if (rotRand % 2 == 0 && _allowRotation)
        {
            rot = Quaternion.Euler(0, 180, 0);
        }
        
        roomTilemap = chooseFrom[rand].GetComponent<Tilemap>();

        Instantiate(chooseFrom[rand], transform.position, rot, transform);
        RandomTileMaker(roomTilemap);
    }

    List<GameObject> RoomCategoryOptions ()
    {
        if (gridID.mazeUnitCode.Contains("N") && gridID.mazeUnitCode.Contains("S"))
        {
            return roomPrefabs.Walkways;
        }

        if (gridID.mazeUnitCode.Contains("E") && gridID.mazeUnitCode.Contains("W"))
        {
            return roomPrefabs.Drops;
        }

        return roomPrefabs.Universal;
    }

    private void WallMaker()
    {
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Vector3Int tilePos = Vector3Int.zero; // ? why do I need to add?
                    tilePos.x = x + bounds.xMin;
                    tilePos.y = y + bounds.yMin;

                    if (!gridID.mazeUnitCode.Contains("W"))
                    {
                        if (tilePos.x == -wallXPos && (tilePos.y >= -wallYPos + 1 && tilePos.y < wallYPos - 1))
                        {    
                            walkableTilemap.SetTile(tilePos, null);
                        }
                    }

                    if (!gridID.mazeUnitCode.Contains("E"))
                    {
                        if (tilePos.x == wallXPos - 1 && (tilePos.y >= -wallYPos + 1 && tilePos.y < wallYPos - 1))
                        {
                            walkableTilemap.SetTile(tilePos, null);
                        }
                    }

                    if (!gridID.mazeUnitCode.Contains("S"))
                    {
                        if (tilePos.y == -wallYPos && (tilePos.x >= -wallXPos + 1 && tilePos.x < wallXPos - 1))
                        {
                            walkableTilemap.SetTile(tilePos, null);
                        }
                    }

                    if (!gridID.mazeUnitCode.Contains("N"))
                    {
                        if (tilePos.y == wallYPos - 1 && (tilePos.x >= -wallXPos + 1 && tilePos.x < wallXPos - 1))
                        {
                            walkableTilemap.SetTile(tilePos, null);
                        }
                    }
                    //Debug.Log("x:" + tilePos.x + " y:" + tilePos.y + " tile:" + tile.name);
                    //var test = tilemap.GetTile(tilePos);
                    //Debug.Log(test.name);	
                    LibUnWalkableTileBases.Add(tile);

                }
                else
                {
                    //Debug.Log("x:" + (x + bounds.xMin) + " y:" + (y + bounds.yMin) + " tile: empty");
                    LibWalkableTileVectors.Add(new Vector2(x + bounds.xMin, y + bounds.yMin));
                }
            }
        }
    }

    private void RandomTileMaker(Tilemap _tilemap)
    {        
        var _bounds = _tilemap.cellBounds;
        var _allTiles = _tilemap.GetTilesBlock(_bounds);
        
        for (int x = 0; x < _bounds.size.x; x++)
        {
            for (int y = 0; y < _bounds.size.y; y++)
            {
                TileBase tile = _allTiles[x + y * _bounds.size.x];
                if (tile != null)
                {
                    Vector3Int tilePos1 = Vector3Int.zero; // because 0 is offset
                    tilePos1.x = x + _bounds.xMin;
                    tilePos1.y = y + _bounds.yMin;
                    
                    // Tile theTile = (Tile)roomTilemap.GetTile(tilePos);
                    // Debug.Log(theTile.name);

                    Debug.Log(tile.name);

                    int rand = Random.Range(0, TileList.Count);

                    Debug.Log(rand);
                    
                    _tilemap.SetTile(tilePos1, TileList[rand]);
                }
            }
        }
    }

    /* 
	void CreateInnerWallsY(bool isEast)
	{
		Vector3Int wallMaker = new Vector3Int();
        wallMaker.z = 0;
		
        if (!isEast)
        {
            wallMaker.x = bounds.xMin + Random.Range(5, 13);
        }
        else
        {
            wallMaker.x = -bounds.xMin - Random.Range(5, 13);
        }

        int startGap = Random.Range(-9, 6);

        List<int> gap = new List<int>();
        for (int i = 0; i < gapSize; i++)
        {
            gap.Add(i + startGap);
        }

		for (int i = bounds.yMin; i < -(bounds.yMin); i++)
		{
			wallMaker.y = i;
            if (!gap.Contains(i))
            {
                tilemap.SetTile(wallMaker, innerWallTileY);
            }
            else
            {
                tilemap.SetTile(wallMaker, null);
            }
		}	
    }

    void CreateInnerWallsX(bool isNorth)
	{
		Vector3Int wallMaker = new Vector3Int();
        wallMaker.z = 0;
		
        if (!isNorth)
        {
            wallMaker.y = -4; //bounds.yMin + Random.Range(3, 5);
        }
        else
        {
            wallMaker.y = 3; //-bounds.yMin - Random.Range(3, 5);

        }

        int startGap = Random.Range(-17, 13);

        List<int> gap = new List<int>();
        for (int i = 0; i < gapSize; i++)
        {
            gap.Add(i + startGap);
        }

		for (int i = bounds.xMin; i < -(bounds.xMin); i++)
		{
			wallMaker.x = i;
            if (!gap.Contains(i))
            {
                tilemap.SetTile(wallMaker, innerWallTileX);
            }
            else
            {
                tilemap.SetTile(wallMaker, null);
            }
		}					
	}
    */  
}
