using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour 
{
    public Text gameCount;
    public Text coinCount;

    void Start()
    {
        gameCount.text = "All Time Levels Played = " + ManagePrefs.MP.GamesPlayed.ToString();
        coinCount.text = "All Time Coins Collected = " + ManagePrefs.MP.Gold.ToString();
    }

    void OnEnable ()
    {
        Time.timeScale = 0f;
    } 

    void OnDisable ()
    {
        Time.timeScale = 1f;
    }

    public void LoadGame ()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadGameMenu ()
    {
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
