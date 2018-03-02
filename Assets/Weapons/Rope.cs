using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour 
{
	public GameObject hook;
    public bool isStopped;
    public LineRenderer line;
    Vector3 playerPos;
    EdgeCollider2D edge2d;
    
    public float maxHeight;

    void Start ()
    {
        Debug.Log("Rope Script Started");
        line = GetComponent<LineRenderer>();
        playerPos = GameObject.Find("Player").transform.position;
        edge2d = GetComponent<EdgeCollider2D>();
    }

    void Update ()
    {
        // Vector2 ropePos = rope.transform.position;
        // ropePos.y = -(rope.transform.lossyScale.y);
        // rope.transform.position = ropePos;
    }
    
    public void RopeReady ()
    {
        isStopped = true;
        edge2d.enabled = true;
        float ropeSize = Vector2.Distance(hook.transform.position, GameObject.Find("Player").transform.position);
        Debug.Log(ropeSize);
        Vector3 ropeEnd = Vector3.zero;
        ropeEnd.y = -ropeSize + 0.5f;
        line.SetPosition(1, ropeEnd);
        
        Vector2[] colliderpoints;
        colliderpoints = edge2d.points;
        colliderpoints[0] = new Vector2(ropeEnd.x, 0);
        colliderpoints [1] = new Vector2 (ropeEnd.x, -ropeSize + 0.5f);
        edge2d.points =  colliderpoints;
    }

    void OnTriggerEnter2D(Collider2D other)
	{
        if (other.tag == "Player")
		{
			maxHeight = hook.transform.position.y;
		}
	}
	
	void OnTriggerStay2D(Collider2D other)
	{
        if (other.tag == "Player" && System.Math.Abs(Input.GetAxis("Vertical")) > 0f)
		{
            Debug.Log("Trigger");

            var playerPos = other.transform.position;
			playerPos.x = transform.position.x;
			if (other.transform.position.y >= maxHeight) 
			{
				playerPos.y = maxHeight;
			}
			other.transform.position = playerPos;

			other.gameObject.GetComponent<CharacterMovement>().isClimbing = true;
		}

        // if (other.tag == "Player" && System.Math.Abs(Input.GetAxis("Horizontal")) > 0)
        // {
        //     other.gameObject.GetComponent<CharacterMovement>().isClimbing = false;
        // }
	}
}
