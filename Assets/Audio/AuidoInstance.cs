using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AuidoInstance : MonoBehaviour 
{    
    public AudioClip jumpSound;
    public AudioClip dropSound;
    public AudioClip climbSound;

    AudioSource audioOne;

    void Awake ()
    {
        audioOne = GameObject.Find("Audio One").GetComponent<AudioSource>();
    }

	void Update () 
    {
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<AudioSource>().PlayOneShot(jumpSound);
        }
	}

}
