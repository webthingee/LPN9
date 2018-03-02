using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour 
{
	[Header("Settings")]
    public LayerMask wallLayer;
        
    [Header("Read-Only Data")]
    [SerializeField] bool wallUp;
    [SerializeField] bool wallRight;
    public bool wallDown;
    [SerializeField] bool wallLeft;
    public int posX;
    public int posY;
    public float floorSize;
    public string tileTypeString;
    public int tileTypeNumber = 40000;

    /// Private Properties
    float rayDist;

    // void Awake ()
    // {
    //     // Establish that the ray for checking walls, is slightly longer than half the width.
    //     //rayDist = (float)GameObject.Find("Maze").GetComponentInParent<GeneratorMaze>().sizeWall * 0.55f;
    //     floorSize = transform.localScale.y;
        
    // }
    
    // void Update()
    // {
    //     rayDist = transform.lossyScale.x / 2;
    //     var rayStart = transform.position + -transform.forward;
    //     var rayDir = transform.right;
    //     Debug.DrawRay(rayStart, rayDir * rayDist, Color.green);
    // }

    void Start ()
    {
        //Invoke("ColorTruths", 1f);
        //Invoke("TouchTruths", 1f);
    }

	/// Send ray in the provided direction and see if it hits a wall (wallLayer)
    bool WallCheck (Vector3 dir)
	{
        rayDist = transform.lossyScale.x / 2;
        Vector3 ctr = transform.position + -transform.forward;
		return Physics.Raycast(ctr, dir, rayDist, wallLayer);
	}

	void ColorTruths ()
	{
        if (WallCheck(transform.up))
            this.GetComponent<Renderer>().material.color = Color.red;
        if (WallCheck(transform.right))
            this.GetComponent<Renderer>().material.color = Color.yellow;

        if (WallCheck(-transform.right))
            this.GetComponent<Renderer>().material.color = Color.magenta;
 
        if (!WallCheck(-transform.up))
            this.GetComponent<Renderer>().material.color = Color.blue;

        if (!WallCheck(-transform.up) && !WallCheck(transform.up))
            this.GetComponent<Renderer>().material.color = Color.blue;

        if (WallCheck(transform.up) && !WallCheck(-transform.up))
            this.GetComponent<Renderer>().material.color = Color.green;
	}

    void TouchTruths()
    {
        if (WallCheck(transform.up)) {
            wallUp = true;
			tileTypeString += "T";
			tileTypeNumber += 1000;
		}
        if (WallCheck(transform.right)) {
            wallRight = true;
			tileTypeString += "R";
            tileTypeNumber += 100;
        }
        if (WallCheck(-transform.up)) {
	        wallDown = true;
			tileTypeString += "D";
            tileTypeNumber += 10;
        }
        if (WallCheck(-transform.right)) {
            wallLeft = true;
			tileTypeString += "L";
            tileTypeNumber += 1;
        }
    }
}
