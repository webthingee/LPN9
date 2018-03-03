using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomPrefabs : MonoBehaviour 
{
	public List<GameObject> Walkways = new List<GameObject>();
    public List<GameObject> Landings = new List<GameObject>();
    public List<GameObject> Drops = new List<GameObject>();
    public List<GameObject> Universal = new List<GameObject>();
    public List<Tile> Tiles = new List<Tile>();
}
