using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
    public GameObject playerSprite;
    
    SpriteRenderer characterSprite;
    Animator anim;
    CharacterMovement cm;
        
    void Awake ()
    {
        cm = GetComponent<CharacterMovement>();
        characterSprite = playerSprite.GetComponent<SpriteRenderer>();
        anim = playerSprite.GetComponent<Animator>();
    }

    void Update ()
    {        
        ChangeDirection(cm.GetMoveDirection.x);

        anim.SetFloat("Forward", Mathf.Abs(cm.GetMoveDirection.x));
    }

    void ChangeDirection (float _direction)
    {        
        /// Rotate Player
        if (_direction > 0) transform.eulerAngles = new Vector3(0, 0, 0);
        if (_direction < 0) transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
