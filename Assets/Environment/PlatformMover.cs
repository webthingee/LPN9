using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour 
{
    public GameObject spot1;
    public GameObject spot2;

    public float speed;
    public Vector2 currentTarget;

    void Start()
    {
        currentTarget = spot1.transform.position;
    }

	void Update () 
    {
        MovePlatform ();
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

	}

    void MovePlatform ()
    {
        if (transform.position == spot1.transform.position)
        {
            currentTarget = spot2.transform.position;
        }
        
        if (transform.position == spot2.transform.position)
        {
            currentTarget = spot1.transform.position;
        }
    }

}
