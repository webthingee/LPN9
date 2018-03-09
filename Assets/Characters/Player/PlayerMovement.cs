using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement 
{	
    protected override void Update () 
    {
        climbAxis = Input.GetAxis("Vertical");
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.x *= speed;
        MoveCharacter();
        base.Update();
	}

    void MoveCharacter ()
    {
        /// Jump : Double Jump
        if (canDoubleJump && Input.GetButtonDown("Jump"))
        {
            if (canDoubleJump)
            {
                moveDirection.y = doubleJumpSpeed;
                canDoubleJump = false;
            }
        }

        /// Wall Climb
        if (Input.GetButton("Utility"))
        {
            if (isRight || isLeft)
            {
                isClimbing = true;
            }
        }
        else
        {
            isClimbing = false;
        }

        if (isClimbing) WallClimbing();

        /// Grounded
        if (isGrounded)
        {
            moveDirection.y = 0;
            isJumping = false;

            if (jumpAvailable && Input.GetAxisRaw("Jump") != 0)
            {
                //moveDirection.y = 0;
                isClimbing = false;
                moveDirection.y = jumpSpeed;
                canDoubleJump = true;
                isJumping = true;
                jumpAvailable = false;
            }
            if (Input.GetAxisRaw("Jump") == 0)
            {
                jumpAvailable = true;
            }
        }

        /// Jump : Hold for full height
        if (Input.GetButtonUp("Jump"))
        {
            if (moveDirection.y > 0)
            {
                moveDirection.y = moveDirection.y * 0.5f;
            }
        }
    }

    private void WallClimbing()
    {
        // @TODO: can still jump up during climb, not sure I like that
        moveDirection.x = 0;
        
        if (WallClimbNorth() || WallClimbSouth())
            moveDirection.y = climbAxis;
        
        if (!WallClimbNorth() && moveDirection.y >= 0)
            moveDirection.y = 0;

        if (!WallClimbSouth() && moveDirection.y <= 0)
            moveDirection.y = 0;

        moveDirection.y *= speed / 4f;

        if (Input.GetAxisRaw("Jump") != 0 && Input.GetAxis("Horizontal") != 0)
        {
            //moveDirection.y = 0;
            isClimbing = false;
            moveDirection.y = jumpSpeed;
            canDoubleJump = true;
            isJumping = true;
            jumpAvailable = false;
        }
    }

    bool WallClimbNorth ()
    {
        var rayStart = transform.position;
        rayStart.x -= 0.5f;
        rayStart.y += 0.1f;
        var rayDir = Vector2.right;
        float rayDist = 1f;

        Debug.DrawRay(rayStart, rayDir * rayDist, Color.green);

        return Physics2D.Raycast(rayStart, rayDir, rayDist, 1 << LayerMask.NameToLayer("Walkable"));
    }

    bool WallClimbSouth ()
    {
        var rayStart = transform.position;
        rayStart.x -= 0.5f;
        rayStart.y -= 0.1f;
        var rayDir = Vector2.right;
        float rayDist = 1f;

        Debug.DrawRay(rayStart, rayDir * rayDist, Color.green);

        return Physics2D.Raycast(rayStart, rayDir, rayDist, 1 << LayerMask.NameToLayer("Walkable"));
    }
}
