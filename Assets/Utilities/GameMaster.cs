using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GameMaster : MonoBehaviour 
{
    public static GameMaster GM = null;

    public GameObject player;
    public GameObject aStar;
    public GameObject pathTakerObj;

    public bool mazeBaseCreated;
    public bool mazeGridCreated;
    public bool mazeStartCreated;
    public bool mazeEndCreated;
    public bool pathDefined;
    public bool roomsBuilt;

    public GameObject startingRoom;
    public GameObject endingRoom;
    public GameObject endingPoint;

    void Awake ()
    {
        //Check if instance already exists
        if (GM == null)
            
            //if not, set instance to this
            GM = this;
        
        //If instance already exists and it's not this:
        else if (GM != this)
            
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);    
        
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        StartCoroutine(ActivateAstar());
        StartCoroutine(BuildRooms());
        StartCoroutine(ActivatePlayer());
    }

    IEnumerator ActivateAstar ()
    {
        yield return new WaitForSeconds(2f);
        
        if (!mazeEndCreated)
            StartCoroutine(ActivateAstar());
        else
            aStar.GetComponent<AstarPath>().Scan();
            PathTaker();
    }

    IEnumerator BuildRooms ()
    {
        yield return new WaitForEndOfFrame();

        if (!pathDefined)
        {
            StartCoroutine(BuildRooms());
        }
        else
        {
            GameObject.Find("Grids").GetComponent<GridPlacement>().AddRooms();
            roomsBuilt = true;
        }
    }

    IEnumerator ActivatePlayer ()
    {
        yield return new WaitForEndOfFrame();

        if (!roomsBuilt)
        {
            StartCoroutine(ActivatePlayer());
        }
        else
        {
            player.transform.position = startingRoom.transform.position;
            player.SetActive(true);
        }
    }

    void PathTaker ()
    {
        //endingPoint = endingRoom.GetComponentInChildren<EndingPoint>().gameObject;
        //endingPoint = GetComponentInChildren<EndingPoint>().gameObject;
        pathTakerObj.GetComponent<AILerp>().destination = endingRoom.transform.position;
        pathTakerObj.transform.position = startingRoom.transform.position;
        pathTakerObj.SetActive(true);
    }
}
