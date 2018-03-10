using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
    public Text healthTXT;
    public Text obolsTXT;

    void Start()
    {
    }

    void Update ()
    {
        healthTXT.text = "All Time Levels Played = " + ManagePrefs.MP.GamesPlayed.ToString();
        obolsTXT.text = "All Time Coins Collected = " + ManagePrefs.MP.Obols.ToString();
    } 
}
