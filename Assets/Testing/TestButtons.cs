using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestButtons : MonoBehaviour
{
    [SerializeField] Text t;    

    void Update()
    {
        t.text = "";
        for (int i = 0; i < 20; i++)
        {
            t.text += "Button " + i + "=" + Input.GetKey("joystick button " + i) + "| ";
        }
    }
}
