using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTest : MonoBehaviour 
{
    void Start()
    {
        SceneManager.LoadScene("Test2", LoadSceneMode.Additive);
    }    
}
