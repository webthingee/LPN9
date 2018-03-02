using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapDanger : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision");
        Destroy(other.gameObject);
    }
}
