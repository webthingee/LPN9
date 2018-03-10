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

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }
	

}
