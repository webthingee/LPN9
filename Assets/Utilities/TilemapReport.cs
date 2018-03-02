using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class TilemapReport : MonoBehaviour 
{
    public List<RoomControl> roomsToEnd = new List<RoomControl>();
    public List<Vector3> pointsToEnd = new List<Vector3>();

    void Update ()
    {
        if (!GetComponent<AILerp>().reachedEndOfPath)
        {
            CheckTilemap(); // @TODO IEnumerator?
        }
    }

    void CheckTilemap ()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 2f, 1 << LayerMask.NameToLayer("Walkable"));
    
        for (var i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponentInParent<RoomControl>() != null)
            {
                if (!roomsToEnd.Contains(hitColliders[i].GetComponentInParent<RoomControl>()))
                    roomsToEnd.Add(hitColliders[i].GetComponentInParent<RoomControl>());
            }            
        }
    }

    void GetGraph ()
    {
        var gg = AstarPath.active.data.gridGraph;
        var zxcv = gg.CountNodes();

        // Debug.Log(zxcv);
        // gg.GetNodes(node => {
        //     // Here is a node
        //     Debug.Log("I found a node at position " + (Vector3)node.position);
        //     if (!pointsToEnd.Contains((Vector3)node.position))
        //         pointsToEnd.Add((Vector3)node.position);
        // });
    }
    

}
