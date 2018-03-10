using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePrefs : MonoBehaviour 
{
    public static ManagePrefs MP = null;

    [SerializeField] int currentObols;
    [SerializeField] int highestObols;
    string obolPP = "Obols";    
    [SerializeField] int allTimeObols;
    string allTimeObolsPP = "AllTimeObols";
    [SerializeField] int gamesPlayed;
    string gamesPlayedPP = "Games_Played";

    public int Obols
    {
        get
        {
            return currentObols;
        }
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

    public void AddAPlayedGame ()
    {
        gamesPlayed++;
        PlayerPrefs.SetInt(gamesPlayedPP, gamesPlayed);
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
        if (!PlayerPrefs.HasKey(obolPP))
        {
            PlayerPrefs.SetInt(obolPP, 0);
        }
        if (!PlayerPrefs.HasKey(gamesPlayedPP))
        {
            PlayerPrefs.SetInt(gamesPlayedPP, 0);
        }
        if (!PlayerPrefs.HasKey(allTimeObolsPP))
        {
            PlayerPrefs.SetInt(allTimeObolsPP, 0);
        }
        
        currentObols = 0;
        gamesPlayed = GetGamesPlayed();
        highestObols = GetHighObols();
        allTimeObols = GetAllTimeObols();
    }

    void CheckHighScore ()
    {
        if (currentObols > GetHighObols())
        {
            PlayerPrefs.SetInt(obolPP, currentObols);
        }
    }

    public int GetHighObols ()
    {
        return PlayerPrefs.GetInt(obolPP); 
    }

    public int GetAllTimeObols ()
    {
        return PlayerPrefs.GetInt(allTimeObolsPP); 
    }

    public int GetGamesPlayed ()
    {
        return PlayerPrefs.GetInt(gamesPlayedPP); 
    }
}
