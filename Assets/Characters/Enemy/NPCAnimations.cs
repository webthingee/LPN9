using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimations : MonoBehaviour 
{
    public SpriteRenderer characterSprite;

    //private CharacterMovement cm;
    private NPCMovement npc;
        
    void Awake ()
    {
        //cm = GetComponent<CharacterMovement>();
        npc = GetComponent<NPCMovement>();
    }

    void Update ()
    {        
        ChangeDirection(npc.VerticalDirection);
    }

    void ChangeDirection (float _direction)
    {        
        /// Rotate Player
        if (_direction > 0) transform.eulerAngles = new Vector3(0, 0, 0);
        if (_direction < 0) transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
