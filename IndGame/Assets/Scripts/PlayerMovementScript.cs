using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour {

	public float walkSpeed = 3f;
	public float jumpPower = 1200f;
    public float punchPower = 100f;
	public LayerMask groundMask;
	public float groundRadius = 0.1f;

	private Rigidbody2D theRigidbody;
    private Transform groundCheck;

    private SpriteRenderer sprRen;
    private bool falling;
    private bool moving;
    private bool attacking;
    private bool onAction;
    private bool jumpStance;
    private bool atkStance;

    private Vector3 resetPosition;
    private Animator anim;
    private float windUp;
    private float lockOut;
    //private int jumpNum = 0;
    //private Text scoreText;

	// Use this for initialization
	void Start () {
        resetPosition = transform.position;
		theRigidbody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
		groundCheck = transform.Find ("groundCheck");
        sprRen = GetComponent<SpriteRenderer>();
        windUp = Time.time;
        //scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis ("Horizontal");
        falling = (theRigidbody.velocity.y < 0) ? true : false;
        //Debug.Log(theRigidbody.velocity.y);
        //Debug.Log(moving);
        onAction = (Time.time - windUp < lockOut) ? true : false;
        //Debug.Log(onAction);
        if (!onAction)
        {
            if (jumpStance)
                jump();

            else if (!falling && !attacking)
            {

                moving = (inputX != 0.0 || theRigidbody.velocity.x != 0) ? true : false;
                theRigidbody.velocity = new Vector2(inputX * walkSpeed, theRigidbody.velocity.y);
                bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
                bool jumping = Input.GetButtonDown("Jump");
                attacking = Input.GetKey(KeyCode.X);

                if (grounded && jumping)
                {
                    jumpStance = true;
                    theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, 0f);
                    anim.SetBool("isJumping", true);
                    windUp = Time.time;
                }

                else if (grounded && attacking)
                {
                    windUp = Time.time;
                    anim.SetBool("isAttacking", attacking);
                    attacking = true;
                    atkStance = true;
                }

                else
                {
                    anim.SetBool("isFalling", false);
                    anim.SetBool("isMoving", moving);
                }

                if (moving)
                    sprRen.flipX = faceDirection(sprRen.flipX, theRigidbody.velocity.x);
            }
            else if (atkStance)
                attack();

            else if (attacking)
            {
                attacking = false;
                anim.SetBool("isAttacking", false);
                anim.SetBool("isMoving", false);
                theRigidbody.velocity = new Vector2(0f, 0f);
            }

            else if (falling)
            {
                theRigidbody.velocity = new Vector2(inputX * walkSpeed, theRigidbody.velocity.y);
                anim.SetBool("isJumping", false);
                anim.SetBool("isFalling", falling);
            }
        }
	}


    private void attack()
    {
        if (Time.time - windUp > 0.8f)
        {
            float direction = (sprRen.flipX) ? -1.0f : 1.0f;
            theRigidbody.AddForce(new Vector2(direction * punchPower * 2, 0));
            atkStance = false;
            anim.SetBool("isMoving", true);
            windUp = Time.time;
            lockOut = 0.1f;
            onAction = true;
        }
    }

    private void jump()
    {
        if (Time.time - windUp > 0.35f)
        {
            theRigidbody.AddForce(new Vector2(0, jumpPower));
            jumpStance = false;
            lockOut = 0.75f;
            //onAction = true;
        }
    }

    private bool faceDirection(bool flip, float direction)
    {
        if (direction == 0)
            return flip;
        else if (direction < 0 && (flip || !flip))
            return true;
        else 
            return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(onAction);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster") && onAction)
            collision.gameObject.GetComponent<MonsterMovement>().death();

    }

}
