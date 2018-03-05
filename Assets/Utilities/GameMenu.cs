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
        gameCount.text = "Games Played = " + ManagePrefs.MP.GamesPlayed.ToString();
        coinCount.text = "Coins Collected = " + ManagePrefs.MP.Gold.ToString();
    }

    public void LoadGame ()
    {
        Debug.Log("LoadGame");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
