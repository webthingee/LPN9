using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePrefs : MonoBehaviour 
{
    public static ManagePrefs MP = null;

    [SerializeField] int obol;
    string obolPP = "Obols";
    [SerializeField] int gamesPlayed;
    string gamesPlayedPP = "Games_Played";

    public int Obols
    {
        get
        {
            return obol;
        }

        set
        {
            obol = value;
            PlayerPrefs.SetInt(obolPP, obol);
        }
    }

    public int GamesPlayed
    {
        get
        {
            return gamesPlayed;
        }

        set
        {
            gamesPlayed = value;
            PlayerPrefs.SetInt(gamesPlayedPP, gamesPlayed);
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
        if (PlayerPrefs.HasKey(obolPP))
        {
            obol = PlayerPrefs.GetInt(obolPP);
        }
        
        if (PlayerPrefs.HasKey(gamesPlayedPP))
        {
            gamesPlayed = PlayerPrefs.GetInt(gamesPlayedPP);
        }
    }

    // void PPLoad ()
    // {
    //     foreach (var pp in PPs())
    //     {
    //         if (PlayerPrefs.HasKey(pp.Key))
    //         {
    //             pp.Value = PlayerPrefs.GetInt(pp.Key);
    //         } 
    //     }
    // }

    // Dictionary<string, int> PPs ()
    // {
    //     Dictionary<string, int> list = new Dictionary<string, int>();
    //         list.Add(goldPP, ref gold);
    //         list.Add(gamesPlayedPP, ref gamesPlayed);

    //     return list;
    // }
    

}
