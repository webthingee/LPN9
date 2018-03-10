using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character 
{	
	public int damage;

    protected override void Start () 
    {
        base.Start();
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        Component damageableComponent = other.gameObject.GetComponent(typeof(IDamageable)); // nullable value
		if (other.gameObject.tag == "Player") 
		{
            if (damageableComponent)
			{
				(damageableComponent as IDamageable).TakeDamage(damage);
			}
        }
    }

}
