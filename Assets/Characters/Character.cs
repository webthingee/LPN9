using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable 
{
    [SerializeField] string charName;
    [SerializeField] float charHealth;
    [SerializeField] float charStrength;

    [SerializeField] float charMaxHealth;
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

    public float CharMaxHealth
    {
        get
        {
            return charMaxHealth;
        }

        set
        {
            charMaxHealth = value;
        }
    }

    protected virtual void Start () 
    {
        sr = GetComponentInChildren<SpriteRenderer>();
	}

    public void TakeDamage(float _amount)
    {
        CharHealth -= _amount;
        DamageResults(this.gameObject.tag);

        if (CharHealth < 0)
        {
            DeathResults(this.gameObject.tag);
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

    void DamageResults (string _tag)
    {
        StartCoroutine(Flash(0.05f, 3));
        
        if (_tag == "Player")
        {
            Debug.Log("player injured");
        }   
    }   
    
    void DeathResults (string _tag)
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity, null);
        GameMaster.GM.ShakeCamera();
        
        if (_tag == "Player")
        {
            GameMaster.GM.GameOverManager();
        }

        if (_tag == "Enemy")
        {
             ManagePrefs.MP.AddKillCount(1);
        }   
    }
}
