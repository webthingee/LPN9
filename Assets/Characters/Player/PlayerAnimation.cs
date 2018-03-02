using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
    public SpriteRenderer characterSprite;

    private CharacterMovement cm;
        
    void Awake ()
    {
        cm = GetComponent<CharacterMovement>();
    }

    void Update ()
    {        
        ChangeDirection(cm.GetMoveDirection.x);
    }

    void ChangeDirection (float _direction)
    {        
        /// Rotate Player
        if (_direction > 0) transform.eulerAngles = new Vector3(0, 0, 0);
        if (_direction < 0) transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
