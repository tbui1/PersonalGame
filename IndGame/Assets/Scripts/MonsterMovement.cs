using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

    private Animator anim;
    private float countDown;
    private bool dead;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        dead = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (dead)
            if (Time.time - countDown > 0.75f)
                Destroy(gameObject);
    }

    public void death()
    {
        if (!dead)
        {
            dead = true;
            countDown = Time.time;
            anim.SetBool("death", true);
        }
    }

}
