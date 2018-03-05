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

        if (isClimbing)
        {
            moveDirection.x = 0;

            moveDirection.y = climbAxis;
            moveDirection.y *= speed / 4f;

            if (Input.GetAxisRaw("Jump") != 0)
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

        // /// Wall Jump and Climb
        // if ((isRight && Input.GetButton("Jump")) || (isLeft && Input.GetButton("Jump"))) // different layer?
        // {
        //     //if (Input.GetButton("Jump"))
        //     //{
        //         stopGravity = true;
        //         moveDirection.y = 0;

        //         canDoubleJump = true; // still in the air
                
        //         //stopGravity = false;
        //         //moveDirection.y = jumpSpeed;
        //         //isJumping = true;
        //     }
        //     else
        //     {
        //         stopGravity = false; 
        //     //}
        // }
    }
}
