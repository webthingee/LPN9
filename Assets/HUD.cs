using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
    [SerializeField] Slider healthSliderGreen;
    [SerializeField] Slider healthSliderYellow;
    [SerializeField] Text obolsTXT;
    [SerializeField] Text killCountTXT;

    void Update ()
    {
        if (!GameMaster.GM.gameIsOver)
        {
            obolsTXT.text = ManagePrefs.MP.ObolsCurrent.ToString();
            killCountTXT.text = ManagePrefs.MP.KillsCurrent.ToString();

            float batteryStrength = GameMaster.GM.player.GetComponent<Player>().CharHealth 
                / GameMaster.GM.player.GetComponent<Player>().CharMaxHealth;

            healthSliderGreen.value = batteryStrength;
            healthSliderYellow.value = batteryStrength + 0.33f;
        }
    } 
}
