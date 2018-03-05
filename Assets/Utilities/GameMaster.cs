using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public class GameMaster : MonoBehaviour 
{
    public static GameMaster GM = null;
    public bool runSetup;

    [Header("Game Objects")]
    public GameObject player;
    public GameObject aStar;
    public GameObject pathTakerObj;
    public GameObject gameMaze;
    public GameObject gameGrid;
    public GameObject loadingCanvas;

    [Header("Dynamic Booleans")]
    public bool mazeBaseCreated;
    public bool mazeGridCreated;
    public bool mazeStartCreated;
    public bool mazeEndCreated;
    public bool pathDefined;
    public bool roomsBuilt;
    public bool playerActive;
    public bool gameInProgress;

    [Header("Dynamic Properties")]
    [SerializeField] public GameObject startingRoom;
    [SerializeField] public GameObject endingRoom;

    void Awake() 
    { 
        if (GM != null && GM != this) 
        { 
            Destroy(this.gameObject);
            return;
        }

        GM = this;
    }

    void Start ()
    {
        AllPropToFalse ();
        PreSetup ();
        
        if (runSetup)
        {
            RunSetup();
        }
    }

    void Update()
    {
        KeyboardInputManager();
    }

    void KeyboardInputManager ()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
        {
            // maybe are you sure menu first? same as pause? instead of pause
            LoadGameMenu();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReLoadScene();
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            PauseGame();
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            ManagePrefs.MP.Gold ++;
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            ManagePrefs.MP.Gold --;
        }
    }

    #region Setup Management
    public void PreSetup ()
    {
        loadingCanvas.SetActive(false);
        gameMaze.SetActive(false);
        gameGrid.SetActive(false);
    }

    public void RunSetup ()
    {
        loadingCanvas.SetActive(true);
        gameMaze.SetActive(true);
        gameGrid.SetActive(true);
        
        StartCoroutine(ActivateAstar());
        StartCoroutine(BuildRooms());
        StartCoroutine(ActivatePlayer());
        StartCoroutine(ShowGame());
    }

    public void ReLoadScene ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    void LoadGameMenu ()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    void PauseGame ()
    {
        // set canvas to active
        // while loop for if canvas is active Time.timescale = 0;
    }

    #endregion

    #region Load Sequence Management
    IEnumerator ActivateAstar ()
    {
        yield return new WaitForSeconds(2f);
        
        if (!mazeEndCreated)
            StartCoroutine(ActivateAstar());
        else
            aStar.GetComponent<AstarPath>().Scan();
            PathTaker();
    }
    
    void PathTaker ()
    {
        pathTakerObj.GetComponent<AILerp>().destination = endingRoom.transform.position;
        pathTakerObj.transform.position = startingRoom.transform.position;
        pathTakerObj.SetActive(true);
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
            playerActive = true;
        }
    }

    IEnumerator ShowGame ()
    {
        yield return new WaitForEndOfFrame();

        if (!playerActive)
        {
            StartCoroutine(ShowGame());
        }
        else
        {
            loadingCanvas.SetActive(false);
            gameInProgress = true;
            ManagePrefs.MP.GamesPlayed++;
        }
    }
    #endregion

    #region Properties Management

    public void AllPropToFalse ()
    {
        foreach (bool prop in PropertiesList())
        {
            PropToFalse(prop);
        }
    }

    public void PropToFalse (bool _boolName)
    {
        _boolName = false;
    }

    List<bool> PropertiesList ()
    {
        List<bool> PropList = new List<bool>();

        PropList.Add(mazeBaseCreated);
        PropList.Add(mazeGridCreated);
        PropList.Add(mazeStartCreated);
        PropList.Add(mazeEndCreated);
        PropList.Add(pathDefined);
        PropList.Add(roomsBuilt);
        PropList.Add(playerActive);
        PropList.Add(gameInProgress);

        return PropList;
    }

    #endregion
}
