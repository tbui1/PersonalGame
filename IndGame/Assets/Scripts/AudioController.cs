using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioClip splash;
    public AudioClip attackSound;
    private AudioSource aud;

    // Use this for initialization
    void Start () {
        aud = GetComponent<AudioSource>();
    }
	
    public void PlayAttackSound(PlayerMovementScript pm)
    {
        aud.PlayOneShot(attackSound, 1f);
    }

    public void PlaySplash(MonsterMovement mm)
    {
        mm.onDeath -= PlaySplash;
        aud.PlayOneShot(splash, 0.5f);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
