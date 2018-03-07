using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour 
{

    void OnTriggerEnter2D(Collider2D other)
    {
		Component damageableComponent = other.gameObject.GetComponent(typeof(IDamageable)); // nullable value
		if (other.tag == "Player")
		{
			if (damageableComponent)
			{
				(damageableComponent as IDamageable).TakeDamage(100);
			}
		}
    }
}
