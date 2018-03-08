﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            {
                Debug.Log("Next Level");
                GameMaster.GM.ReLoadScene();  
            }
    }
}
