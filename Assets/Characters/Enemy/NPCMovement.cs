using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : CharacterMovement 
{	
    public float edgeDistance;

    public LayerMask walkable;
    private int verticalDirection = 1;

    public int VerticalDirection
    {
        get
        {
            return verticalDirection;
        }

        set
        {
            verticalDirection = value;
        }
    }

    protected override void Update () 
    {
        if (!SomethingEast() || WallEast()) { verticalDirection = -1; }
        if (!SomethingWest() || WallWest()) { verticalDirection = 1; }         
        MoveCharacter ();

        base.Update();
	}

    void MoveCharacter ()
    {
        /// Jump : Double Jump
        // if (canDoubleJump && Input.GetButtonDown("Fire1"))
        // {
        //     if (canDoubleJump)
        //     {
        //         moveDirection.y = doubleJumpSpeed;
        //         canDoubleJump = false;
        //     }
        // }

        /// Grounded
        // if (isGrounded)
        // {
            moveDirection.x = verticalDirection;
            moveDirection.x *= speed;
            moveDirection.y = 0;
            //isJumping = false;

        //     // if (jumpAvailable && Input.GetAxisRaw("Fire1") != 0)
        //     // {
        //     //     //isClimbing = false;
        //     //     moveDirection.y = jumpSpeed;
        //     //     canDoubleJump = true;
        //     //     isJumping = true;
        //     //     jumpAvailable = false;
        //     // }
        //     // if (Input.GetAxisRaw("Fire1") == 0)
        //     // {
        //     //     jumpAvailable = true;
        //     // }
        //}
        // else
        // {
        //     moveDirection.x = -1f;
        //     moveDirection.x *= speed;
        //     moveDirection.y = 0;
        // }

        
        /// Jump : Hold for full height
        // if (Input.GetButtonUp("Fire1"))
        // {
        //     if (moveDirection.y > 0)
        //     {
        //         moveDirection.y = moveDirection.y * 0.5f;
        //     }
        // }
    }

    bool SomethingEast ()
    {
        var rayStart = transform.position;
        rayStart.x += edgeDistance;
        var rayDir = Vector2.down;
        float rayDist = .5f;

        Debug.DrawRay(rayStart, rayDir * rayDist, Color.green);

        return Physics2D.Raycast(rayStart, rayDir, rayDist, walkable);
    }

    bool WallEast ()
    {
        var rayStart = transform.position;
        var rayDir = Vector2.right;
        float rayDist = .5f;

        Debug.DrawRay(rayStart, rayDir * rayDist, Color.green);

        return Physics2D.Raycast(rayStart, rayDir, rayDist, walkable);
    }

    bool SomethingWest ()
    {
        var rayStart = transform.position;
        rayStart.x -= edgeDistance;
        var rayDir = Vector2.down;
        float rayDist = .5f;

        Debug.DrawRay(rayStart, rayDir * rayDist, Color.green);

        return Physics2D.Raycast(rayStart, rayDir, rayDist, walkable);
    }

        bool WallWest ()
    {
        var rayStart = transform.position;
        var rayDir = -Vector2.right;
        float rayDist = .5f;

        Debug.DrawRay(rayStart, rayDir * rayDist, Color.green);

        return Physics2D.Raycast(rayStart, rayDir, rayDist, walkable);
    }
}
