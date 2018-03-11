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
    public GameObject startingRoom;
    public bool isEndingRoom;
    public GameObject endingRoom;
    public bool isOnCompletionPath;
    public bool isRotated;

    [Header("Room Settings")]
    public bool addRooms;
    public bool testmode;
    public GameObject exitPrefab;
    public GameObject lightPrefab;
    public RoomPrefabs roomPrefabs;
    public List<Tile> TileList = new List<Tile>();
    public Tile randomTile;
    public GameObject randomPrefab;

	private List<Vector2> LibWalkableTileVectors = new List<Vector2>();
	private List<TileBase> LibUnWalkableTileBases = new List<TileBase>();
    private GridID gridID;

	void Start ()
    {
        gridID = GetComponent<GridID>();

        WallMaker();

        if (testmode)
        {
            RoomSelection();
        }
        
    }

    public void SetupOnPath ()
    {
        isOnCompletionPath = true;
    }

    public void RoomSelection()
    {
        List<GameObject> chooseRoomFrom = RoomCategoryOptions();
        
        int rand = Random.Range(0, chooseRoomFrom.Count);
                
        roomTilemap = chooseRoomFrom[rand].GetComponent<Tilemap>();

        GameObject choosenRoom = Instantiate(chooseRoomFrom[rand], transform.position, Quaternion.identity, transform);

        if (choosenRoom.GetComponent<RoomContents>().canFlipXAxis)
        {
            int rotRand = Random.Range(0, 101);
            if (rotRand % 2 == 0)
            {
                choosenRoom.transform.rotation = Quaternion.Euler(0, 180, 0);
                isRotated = true;
            }
        }
        RandomTileMaker(roomTilemap);
        PlaceStuff(roomTilemap);
    }

    List<GameObject> RoomCategoryOptions ()
    {
        switch (gridID.passedGridID)
        {
            case 20101: // East and West Only
                return roomPrefabs.r20101;
            case 21010: // North and West Only
                return roomPrefabs.r21010;            
            case 20110: // East and Soutn Only
                return roomPrefabs.r20110;

            case 30111: // Open North only
                return roomPrefabs.r30111;

            default:
                return roomPrefabs.Universal;
        }
    }

    private void WallMaker()
    {
        var bounds = walkableTilemap.cellBounds;
        var allTiles = walkableTilemap.GetTilesBlock(bounds);
        
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

                    int rand = Random.Range(0, TileList.Count);
                    
                    _tilemap.SetTile(tilePos1, TileList[rand]);
                }
            }
        }
    }

    #region     Random Open Spot

    private List<Vector3Int> RandomOpenSpots(Tilemap _tilemap) // multiple include hazard
    {        
        var _bounds = _tilemap.cellBounds;
        var _allTiles = _tilemap.GetTilesBlock(_bounds);
        List<Vector3Int> openTiles = new List<Vector3Int>();
        
        for (int x = 0; x < _bounds.size.x; x++)
        {
            for (int y = 0; y < _bounds.size.y; y++)
            {
                TileBase tile = _allTiles[x + y * _bounds.size.x];
                
                if (tile == null)
                {
                    Vector3Int tilePos = Vector3Int.zero; // because 0 is offset
                    tilePos.x = x + _bounds.xMin;
                    tilePos.y = y + _bounds.yMin;

                    if (-6 < tilePos.x && tilePos.x < 6)
                    {
                        if (-4 < tilePos.y && tilePos.y < 4)
                        {
                            openTiles.Add(tilePos);
                        }                    
                    }
                }
            }
        }
        return openTiles;
    }

    private Vector3Int RandomOpenSpot(Tilemap _tilemap)
    {
        var openSpots = RandomOpenSpots(_tilemap);

        int rand = Random.Range(0, openSpots.Count);
        
        return openSpots[rand];
    }

    private void RandomOpenTilePlacement(Tilemap _tilemap, Tile _tile)
    {
        var openSpot = RandomOpenSpot(_tilemap);
        _tilemap.SetTile(openSpot, _tile);
    }

    private void RandomOpenPrefabPlacement(Tilemap _tilemap, GameObject _preFab)
    {
        Vector3 openSpot = RandomOpenSpot(_tilemap);
        openSpot.x += 0.5f;
        openSpot.y += 0.5f;

        if (isRotated)
        {
            openSpot.x *= -1;            
        }

        // Vector3Int cellPosition = _tilemap.WorldToCell(openSpot);
        // var prefabPos = _tilemap.GetCellCenterWorld(cellPosition);

        GameObject ep = Instantiate(_preFab, openSpot, Quaternion.identity, this.transform);
        ep.transform.localPosition = openSpot;

    }
    #endregion

    #region     Random Tiled Point

    private List<Vector3Int> RandomTiledSpots(Tilemap _tilemap) // multiple include hazard
    {        
        var _bounds = _tilemap.cellBounds;
        var _allTiles = _tilemap.GetTilesBlock(_bounds);
        List<Vector3Int> TilePlaced = new List<Vector3Int>();
        
        for (int x = 0; x < _bounds.size.x; x++)
        {
            for (int y = 0; y < _bounds.size.y; y++)
            {
                TileBase tile = _allTiles[x + y * _bounds.size.x];
                
                if (tile != null)
                {
                    Vector3Int tilePos = Vector3Int.zero; // because 0 is offset
                    tilePos.x = x + _bounds.xMin;
                    tilePos.y = y + _bounds.yMin;

                    if (-6 < tilePos.x && tilePos.x < 6)
                    {
                        if (-4 < tilePos.y && tilePos.y < 4)
                        {
                            TilePlaced.Add(tilePos);
                        }                    
                    }
                }
            }
        }
        return TilePlaced;
    }

    private Vector3Int RandomTiledSpot(Tilemap _tilemap)
    {
        var tiledSpots = RandomTiledSpots(_tilemap);

        int rand = Random.Range(0, tiledSpots.Count);
        
        return tiledSpots[rand];
    }

    private void RandomTiledPrefabPlacement(Tilemap _tilemap, GameObject _preFab)
    {
        Vector3 TiledSpot = RandomTiledSpot(_tilemap);
        TiledSpot.x += 0.5f;
        TiledSpot.y += 0.5f;

        if (isRotated)
        {
            TiledSpot.x *= -1;            
        }

        GameObject ep = Instantiate(_preFab, TiledSpot, Quaternion.identity, this.transform);
        ep.transform.rotation = Quaternion.Euler(0,0,0);
        ep.transform.localPosition = TiledSpot;

    }

    #endregion

    void PlaceStuff (Tilemap _tilemap)
    {
        //RandomOpenPrefabPlacement(_tilemap, randomPrefab);

        if (isEndingRoom)
        {
            var rc = GetComponentInChildren<RoomContents>();
            rc.DepositAtRewardPoint(exitPrefab, true);
        }

        // Place a light if on the path to the exit
        if (isOnCompletionPath)
        {
            RandomTiledPrefabPlacement(_tilemap, lightPrefab); //orig
        }
    }
}
