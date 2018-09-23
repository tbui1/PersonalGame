using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour {

	public float walkSpeed = 3f;
	public float jumpPower = 600f;

	public LayerMask groundMask;
	public float groundRadius = 0.1f;

	private Rigidbody2D theRigidbody;
    private Transform groundCheck;

    private SpriteRenderer sprRen;
    private bool falling;
    private bool moving;
    private Vector3 resetPosition;
    private Animator anim;

    //private int jumpNum = 0;
    //private Text scoreText;

	// Use this for initialization
	void Start () {
        resetPosition = transform.position;
		theRigidbody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
		groundCheck = transform.Find ("groundCheck");
        sprRen = GetComponent<SpriteRenderer>();
        //scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis ("Horizontal");
        falling = (theRigidbody.velocity.y < 0) ? true : false;
        Debug.Log(theRigidbody.velocity.y);
        Debug.Log(falling);
        if (!falling)
        {

            moving = (inputX != 0.0) ? true : false;
            theRigidbody.velocity = new Vector2(inputX * walkSpeed, theRigidbody.velocity.y);

            bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
            bool jumping = Input.GetButtonDown("Jump");
            if (grounded && jumping)
            {
                theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, 0f);
                theRigidbody.AddForce(new Vector2(0, jumpPower));
                //jumpNum++;
                //scoreText.text = "# of Jumps: " + jumpNum;
            }
            if (jumping)
                Debug.Log(jumping);
            anim.SetBool("isFalling", false);
            anim.SetBool("isJumping", !grounded);
            anim.SetBool("isMoving", moving);
            if (moving)
                sprRen.flipX = (inputX < 0) ? true : false;
        }
        else
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", falling);
            
        }//Debug.Log(direction);
	}

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Death"))
        {
            GetComponent<AudioSource>().Play();
            transform.position = resetPosition;
        }
    }*/

}
