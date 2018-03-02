using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomControl : MonoBehaviour 
{
	//public RoomStuff roomStuff;
    [Header("Tilemap Settings")]
    public Tilemap walkableTilemap;
    public int wallXPos = 10;
    public int wallYPos = 10;
	public int openingSize;
    
    [Header("Room Settings")]
    public int placementX;
    public int placementY;
    public bool isStartingRoom;
    public bool isEndingRoom;

    [Header("Room Settings")]
    public bool addRooms;
    public GameObject[] roomOptions;

	private List<Vector2> LibWalkableTileVectors = new List<Vector2>();
	private List<TileBase> LibUnWalkableTileBases = new List<TileBase>();
	BoundsInt bounds;
	TileBase[] allTiles;
	
	void Awake()
	{
        openingSize = openingSize/2;
	}

	void Start ()
    {
        bounds = walkableTilemap.cellBounds;
        allTiles = walkableTilemap.GetTilesBlock(bounds);

        WallMaker();

        if (roomOptions.Length > 0 && addRooms)
        {
            int rand = Random.Range(0, roomOptions.Length);
            int rotRand = Random.Range(0, 101);
            Quaternion rot = Quaternion.Euler(0,180,0);
            if (rotRand % 2 == 0)
            {
                rot = Quaternion.identity;
            }
            Instantiate(roomOptions[rand], transform.position, rot, transform);
        }
        //Instantiate(roomStuff.ememies[0], transform.position, Quaternion.identity);

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

                    var id = GetComponentInParent<GridID>().passedGridID;

                    if (!GetComponentInParent<GridID>().mazeUnitCode.Contains("W"))
                    {
                        if (tilePos.x == -wallXPos && (tilePos.y >= -openingSize && tilePos.y < openingSize))
                        {    
                            walkableTilemap.SetTile(tilePos, null);
                        }
                    }

                    if (!GetComponentInParent<GridID>().mazeUnitCode.Contains("E"))
                    {
                        if (tilePos.x == wallXPos - 1 && (tilePos.y >= -openingSize && tilePos.y < openingSize))
                        {
                            walkableTilemap.SetTile(tilePos, null);
                        }
                    }

                    if (!GetComponentInParent<GridID>().mazeUnitCode.Contains("S"))
                    {
                        if (tilePos.y == -wallYPos && (tilePos.x >= -openingSize && tilePos.x < openingSize))
                        {
                            walkableTilemap.SetTile(tilePos, null);
                        }
                    }

                    if (!GetComponentInParent<GridID>().mazeUnitCode.Contains("N"))
                    {
                        if (tilePos.y == wallYPos - 1 && (tilePos.x >= -openingSize && tilePos.x < openingSize))
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
