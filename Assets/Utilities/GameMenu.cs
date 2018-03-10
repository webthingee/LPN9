﻿using System.Collections;
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
        coinCount.text = "All Time Coins Collected = " + ManagePrefs.MP.Obols.ToString();
    }

    void OnEnable ()
    {
        Time.timeScale = 0f;
        gameCount.text = "All Time Levels Played = " + ManagePrefs.MP.GamesPlayed.ToString();
        coinCount.text = "All Time Coins Collected = " + ManagePrefs.MP.Obols.ToString();
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
