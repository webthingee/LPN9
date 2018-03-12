using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ManagePrefs : MonoBehaviour 
{
    public static ManagePrefs MP = null;

    [Header("Player Settings")]
    public bool easyMode = true;    
    public bool muteAudio = false;

    [Header("Player Pref Values")]
    [SerializeField] int gamesPlayed;
    string gamesPlayedPP = "Games_Played"; 
    
    [SerializeField] int highestObols;
    string obolPP = "Obols";    
    [SerializeField] int allTimeObols;
    string allTimeObolsPP = "AllTimeObols";
    
    [SerializeField] int highestKills;
    string killsPP = "Kills";
    [SerializeField] int allTimeKills;
    string allTimeKillsPP = "Kill_Count_All_Time";

    [SerializeField] int currentObols = 0;
    [SerializeField] int currentKills = 0;


    public int ObolsCurrent
    {
        get
        {
            return currentObols;
        }
        set
        {
            currentObols = value;
        }
    }

    public int KillsCurrent
    {
        get
        {
            return currentKills;
        }
        set
        {
            currentKills = value;
        }
    }

    void Awake()
    {
        if (MP == null)
            MP = this;
        
        else if (MP != this)
            Destroy(gameObject);    
        
        DontDestroyOnLoad(gameObject);

        // One Time Setup
        Init();
    }

    void Init ()
    {
        easyMode = true;
        muteAudio = false;
    }

    public void AddObols (int _value)
    {
        currentObols += _value;
        CheckHighScore();
        
        int currentAllTimeObols = PlayerPrefs.GetInt(allTimeObolsPP);
        currentAllTimeObols += _value;

        PlayerPrefs.SetInt(allTimeObolsPP, currentAllTimeObols);
        allTimeObols = PlayerPrefs.GetInt(allTimeObolsPP);
    }

    public void AddKillCount (int _value)
    {
        currentKills += _value;
        CheckHighKills();
        
        int currentAllTimeKills = PlayerPrefs.GetInt(allTimeKillsPP);
        currentAllTimeKills += _value;

        PlayerPrefs.SetInt(allTimeKillsPP, currentAllTimeKills);
        allTimeKills = PlayerPrefs.GetInt(allTimeKillsPP);
    }

    public void AddAPlayedGame ()
    {
        gamesPlayed++;
        PlayerPrefs.SetInt(gamesPlayedPP, gamesPlayed);
    }

    private void PlayerPrefsManage ()
    {
        PlayerPrefsStart(obolPP);
        PlayerPrefsStart(gamesPlayedPP);
        PlayerPrefsStart(allTimeObolsPP);
        PlayerPrefsStart(allTimeKillsPP);
        PlayerPrefsStart(killsPP);
    }

    public void PlayerPrefsRestart ()
    {
        PlayerPrefsReset(obolPP);
        PlayerPrefsReset(gamesPlayedPP);
        PlayerPrefsReset(allTimeObolsPP);
        PlayerPrefsReset(allTimeKillsPP);
        PlayerPrefsReset(killsPP);
    }

    private void PlayerPrefsStart (string _pref)
    {
        if (!PlayerPrefs.HasKey(_pref))
        {
            PlayerPrefs.SetInt(_pref, 0);
        }
    }

    private void PlayerPrefsReset (string _pref)
    {
        if (PlayerPrefs.HasKey(_pref))
        {
            PlayerPrefs.SetInt(_pref, 0);
        }
    }

    void CheckHighScore ()
    {
        if (currentObols > GetHighObols())
        {
            PlayerPrefs.SetInt(obolPP, currentObols);
        }
    }

    void CheckHighKills ()
    {
        if (currentKills > GetHighKills())
        {
            PlayerPrefs.SetInt(killsPP, currentKills);
        }
    }

    public int GetHighObols ()
    {
        highestObols = PlayerPrefs.GetInt(obolPP);
        return highestObols; 
    }

    public int GetHighKills ()
    {
        highestKills = PlayerPrefs.GetInt(killsPP);
        return highestKills; 
    }

    public int GetAllTimeObols ()
    {
        allTimeObols =PlayerPrefs.GetInt(allTimeObolsPP);
        return allTimeObols; 
    }

    public int GetAllTimeKills ()
    {
        allTimeKills = PlayerPrefs.GetInt(allTimeKillsPP);
        return allTimeKills;
    }

    public int GetGamesPlayed ()
    {
        gamesPlayed = PlayerPrefs.GetInt(gamesPlayedPP); 
        return gamesPlayed;
    }
}
