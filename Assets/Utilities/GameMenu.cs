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

    public Text recapTXT;

    public Toggle easy;
    public Toggle mute;

    [Header("Additional Canvas")]
    public GameObject storyCanvas;
    public bool showStoryPanel = false;


    void Awake ()
    {
        gameCount.text = "Total Games = " + ManagePrefs.MP.GetGamesPlayed().ToString();
        
        obolHighCount.text = "Highest Obols = " + ManagePrefs.MP.GetHighObols().ToString();
        obolAllTimeCount.text = "All Time Obols = " + ManagePrefs.MP.GetAllTimeObols().ToString();
    
        killsHighCount.text = "Highest Kills = " + ManagePrefs.MP.GetHighKills().ToString();
        killsAllTimeCount.text = "All Time Kills = " + ManagePrefs.MP.GetAllTimeKills().ToString();

        if (ManagePrefs.MP.KillsCurrent > 0 || ManagePrefs.MP.ObolsCurrent > 0)
        {
            recapTXT.text = 
            "You have begun this adventure " + ManagePrefs.MP.GetGamesPlayed().ToString() + " times." 
            + "\n" + "This time you defeated " + ManagePrefs.MP.KillsCurrent + " DragZards."
            + "\n" + "You also gathered " + ManagePrefs.MP.ObolsCurrent + " Obols";
        }

        easy.isOn = ManagePrefs.MP.easyMode;
        mute.isOn = ManagePrefs.MP.muteAudio;
        

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

    public void PanelToggle (GameObject _canvas)
    {
        showStoryPanel = !showStoryPanel;
        _canvas.SetActive(showStoryPanel);
    }


    public void MuteAudio ()
    {
        ManagePrefs.MP.muteAudio = ! ManagePrefs.MP.muteAudio;

        if (ManagePrefs.MP.muteAudio)
        {
            AudioListener.volume = 0f;
            AudioListener.pause = true;
        }
        else 
        {
            AudioListener.volume = 1f;
            AudioListener.pause = false;
        }
    }

    public void EasyMode ()
    {
        ManagePrefs.MP.easyMode = ! ManagePrefs.MP.easyMode;
    }
}
