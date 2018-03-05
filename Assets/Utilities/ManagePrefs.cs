using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePrefs : MonoBehaviour 
{
    public static ManagePrefs MP = null;

    public int gold;

    void Awake()
    {
        if (MP == null)
            MP = this;
        
        else if (MP != this)
            Destroy(gameObject);    
        
        DontDestroyOnLoad(gameObject);
    }
    

}
