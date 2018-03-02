using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Accessories/Weapon"))]
public class Weapon : ScriptableObject
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] public GameObject projectilePrefab;
    
    [SerializeField] AnimationClip attackAnimation;
    
    [SerializeField] float timeBetweenHits = 0.5f;
    [SerializeField] float range = 2f;

    #region Getters and Setters
    public GameObject GetWeapon
    {
        get { return weaponPrefab; }
    }

    public GameObject GetProjectile
    {
        get { return projectilePrefab; }
    }

    public float TimeBetweenHits
    {
        get
        {
            return timeBetweenHits;
        }

        set
        {
            timeBetweenHits = value;
        }
    }

    public float Range
    {
        get
        {
            return range;
        }

        set
        {
            range = value;
        }
    }
    #endregion


}