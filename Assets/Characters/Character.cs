using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable 
{
    [SerializeField] string charName;
    [SerializeField] float charHealth;
    [SerializeField] float charStrength;
    [SerializeField] int baseDamage = 1;

    public GameObject deathEffect;

    SpriteRenderer sr;

    bool isQuitting;

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

    protected virtual void Start () 
    {
        sr = GetComponentInChildren<SpriteRenderer>();
	}

    public void TakeDamage(float _amount)
    {
        CharHealth -= _amount;
        StartCoroutine(Flash(0.05f, 3));

        if (CharHealth < 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity, null);
            GameMaster.GM.ShakeCamera();
            GameMaster.GM.GameOverManager();
            Destroy(this.gameObject);
        }
    }

    // void OnApplicationQuit ()
    // {
    //     isQuitting = true;
    // }

    // void OnDestroy () // https://answers.unity.com/questions/169656/instantiate-ondestroy.html
    // {
    //     if (!isQuitting && deathEffect != null)
    //         Instantiate(deathEffect, transform.position, Quaternion.identity, null);
    // }

     IEnumerator Flash(float _wait, int _flashes) 
     {
        Color origColor = sr.color;
        for (int i = 0; i < _flashes; i++) 
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(_wait);
            sr.color = origColor;
            yield return new WaitForSeconds(_wait);
        }
        sr.color = origColor;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Component damageableComponent = other.gameObject.GetComponent(typeof(IDamageable)); // nullable value
		if (other.gameObject.tag == "Player")
		{
			if (damageableComponent)
			{
				(damageableComponent as IDamageable).TakeDamage(baseDamage);
			}
        }
    }

}
