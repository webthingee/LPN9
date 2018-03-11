using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour 
{
    public Text gameCount;
    public Text obolHighCount;
    public Text obolAllTimeCount;
    public Text killsHighCount;
    public Text killsAllTimeCount;

    void OnEnable ()
    {
        gameCount.text = "Games = " + ManagePrefs.MP.GetGamesPlayed().ToString();
        
        obolHighCount.text = "Highest Obols = " + ManagePrefs.MP.GetHighObols().ToString();
        obolAllTimeCount.text = "All Time Obols = " + ManagePrefs.MP.GetAllTimeObols().ToString();
    
        killsHighCount.text = "Highest Kills = " + ManagePrefs.MP.GetHighKills().ToString();
        killsAllTimeCount.text = "All Time Kills = " + ManagePrefs.MP.GetAllTimeKills().ToString();

        Time.timeScale = 0f;
    } 

    void OnDisable ()
    {
        Time.timeScale = 1f;
    }

    public void LoadGame ()
    {
        Debug.Log("Load Game");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadGameMenu ()
    {
        Debug.Log("Load Game Menu");
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void QuitGame ()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
     }

    public void CloseCanvas ()
    {
        this.gameObject.SetActive(false);
    }
}
