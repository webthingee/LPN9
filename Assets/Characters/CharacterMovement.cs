using System;
using System.Collections;
using System.Collections.Generic;
using Prime31;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Character Movement")]
    public float speed = 12.0f;
    public float jumpSpeed = 24.0f;
    public float doubleJumpSpeed = 24.0f;

    [Header("Environmental")]
    public float gravity = 36.0f;
    public bool isClimbing = false;

    protected Vector3 moveDirection = Vector3.zero;
    [SerializeField] protected float climbAxis;
    [SerializeField] protected bool isJumping;
    [SerializeField] protected bool jumpAvailable;
    [SerializeField] protected bool canDoubleJump;
    [SerializeField] protected bool hasDoubleJumped;
    [SerializeField] protected bool stopGravity;
    
    CharacterController2D cc2d;
    CharacterController2D.CharacterCollisionState2D flags;
    [SerializeField] protected bool isGrounded;
    [SerializeField] protected bool isRight;
    [SerializeField] protected bool isLeft;

    public Vector3 GetMoveDirection
    {
        get
        {
            return moveDirection;
        }
    }

    void Awake ()
	{
        cc2d = GetComponent<CharacterController2D>();
    }

    protected virtual void Update ()
    {
        // Gravity and Movement
        // if (!stopGravity)
        // {
        //     moveDirection.y -= gravity * Time.deltaTime;
        //     // moving left or right (or jump or fall)
        //     cc2d.move(moveDirection * Time.deltaTime);
        // }


        //if (!stopGravity || isClimbing) // order??????
        if (!isClimbing) // order??????
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        cc2d.move(moveDirection * Time.deltaTime);

        // report what is around us
        flags = cc2d.collisionState;

        /// Check if we are on the ground
        isGrounded = flags.below;
        isRight = flags.right;
        isLeft = flags.left;

        /// Check if we hit our head
        if (flags.above)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            moveDirection.x = 0;
        }
    }
}