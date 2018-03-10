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
        healthTXT.text = "Life : ";
        obolsTXT.text = "Obols = " + ManagePrefs.MP.Obols.ToString();
    } 
}
