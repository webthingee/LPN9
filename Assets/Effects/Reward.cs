using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour 
{
    public int reward;
    public GameObject impactEffect;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			ManagePrefs.MP.AddObols(1);
            Impact();
		}
	}

	void Impact()
	{
		Instantiate(impactEffect, transform.position, transform.rotation);        
		Destroy(this.gameObject);
	}
}
