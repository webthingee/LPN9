using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;
using Cinemachine;

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
    public GameObject cameras;
    public GameObject loadingCanvas;
    public GameObject pauseCanvas;
    public GameObject gameOverCanvas;

    [Header("Dynamic Booleans")]
    public bool mazeBaseCreated;
    public bool mazeGridCreated;
    public bool mazeStartCreated;
    public bool mazeEndCreated;
    public bool pathDefined;
    public bool roomsBuilt;
    public bool playerActive;
    public bool gameInProgress;
    public bool gameIsOver;

    [Header("Dynamic Properties")]
    [SerializeField] public GameObject startingRoom;
    [SerializeField] public GameObject endingRoom;
    CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;
    private bool isShaking;

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
            StartCoroutine(CameraShake(0.4f, 0.2f, 0.2f));
        }
    }

    #region         Setup Management

    public void PreSetup ()
    {
        pauseCanvas.SetActive(false);
        loadingCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
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
        pauseCanvas.SetActive(true);
    }

    public void GameOverManager ()
    {
        GameMaster.GM.gameIsOver = true;
        StartCoroutine(GameOver(1f));
    }

    #endregion

    #region         Load Sequence Management

    IEnumerator ActivateAstar ()
    {
        yield return new WaitForSeconds(1.5f);
        
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
            var rc = startingRoom.GetComponentInChildren<RoomContents>();
            var placePlayer = rc.GetDepositPoint(rc.rewardSport, true);

            player.transform.position = (Vector2)placePlayer;
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
            ManagePrefs.MP.AddAPlayedGame();
        }
    }
    #endregion

    #region         Properties Management

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
        PropList.Add(gameIsOver);

        return PropList;
    }

    #endregion

    public void ShakeCamera ()
    {
        StartCoroutine(CameraShake(0.4f, 0.2f, 0.5f));
    }

    public IEnumerator CameraShake (float _duration, float _magnitude, float _freq)
    {
        isShaking = true;
        vcam = GameObject.Find("Camera Control").GetComponent<CinemachineVirtualCamera> ();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        float elpased = 0.0f;

        while (elpased < _duration)
        {
            noise.m_AmplitudeGain = Random.Range(-1, 1) * _magnitude;
            noise.m_FrequencyGain = Random.Range(-1, 1) * _freq; 
            
            elpased += Time.deltaTime;
            isShaking = true;

            yield return null;
        }

        isShaking = false;

        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }

    IEnumerator GameOver (float _wait)
    {
        yield return new WaitForSeconds(_wait);
        
        gameOverCanvas.SetActive(true);

        yield return new WaitForSeconds(_wait * 2);

        if (!isShaking)
        {
            LoadGameMenu();
        }
        else
        {
            StartCoroutine(GameOver(_wait));
        }
    }

}
