using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character 
{
	protected override void Start () 
    {
        base.Start();
	}

    void OnDestroy()
    {
        Debug.Log("Destroyed");
    }
	

}
