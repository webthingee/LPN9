using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hook : MonoBehaviour 
{
    public LayerMask layerMask;
    public Rope rope;

    void Awake()
    {
        rope = GetComponentInChildren<Rope>();
    }
    // void Update()
    // {
    //     Vector2 hookUp = transform.position;
    //     //hookUp.x = Mathf.Round(hookUp.x * 2f) * 0.5f;
    //     hookUp.x = Mathf.Round(hookUp.x) + 0.5f;
    //     transform.position = hookUp;
    // }

	void OnTriggerEnter2D(Collider2D other)
    {
        if (1 << other.gameObject.layer == 1 << LayerMask.NameToLayer("Walkable"))
        {
            GetComponent<Projectile>().projectileSpeed = 0f;
            rope.RopeReady();
        }
    }
}
