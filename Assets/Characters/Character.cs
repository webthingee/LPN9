using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable 
{
    [SerializeField] string charName;
    [SerializeField] float charHealth;
    [SerializeField] float charStrength;

    public float CharHealth
    {
        get
        {
            return charHealth;
        }

        set
        {
            charHealth = value;
        }
    }

    void Start () 
    {
		
	}
	
	void Update () 
    {
		
	}

    public void TakeDamage(float _amount)
    {
        CharHealth -= _amount;
        if (CharHealth < 0)
        {
            Destroy(this.gameObject);
        }
    }

}
