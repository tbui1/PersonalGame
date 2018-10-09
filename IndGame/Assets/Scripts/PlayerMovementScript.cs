using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum State {Standing, Walking, Jumping, Falling, Attacking}

public class PlayerMovementScript : MonoBehaviour {

    public delegate void OnAttack(PlayerMovementScript mm);
    public event OnAttack onAttack;
    public AudioController ac;

    public float walkSpeed = 3f;
    public float jumpPower = 1200f;
    public float punchPower = 100f;
    public LayerMask groundMask;
    public float groundRadius = 0.1f;

    private Rigidbody2D theRigidbody;
    private Transform groundCheck;

    private SpriteRenderer sprRen;
    private State action;
    private BoxCollider2D punchHB;
    private CircleCollider2D jumpHB;
    private Vector3 resetPosition;
    private Animator anim;
    //private int jumpNum = 0;
    //private Text scoreText;

    // Use this for initialization
    void Start() {
        punchHB = GetComponent<BoxCollider2D>();
        jumpHB = GetComponent<CircleCollider2D>();
        punchHB.enabled = false;
        jumpHB.enabled = false;
        action = State.Walking;
        resetPosition = transform.position;
		theRigidbody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
		groundCheck = transform.Find ("groundCheck");
        sprRen = GetComponent<SpriteRenderer>();
        onAttack += ac.PlayAttackSound;

        //scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        switch (action)
        {
            case State.Walking:
                {
                    //bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
                    bool jumping = Input.GetButtonDown("Jump");
                    bool attacking = Input.GetKey(KeyCode.X);
                    if (jumping)
                    {
                        action = State.Jumping;
                        anim.SetBool("isJumping", true);
                        theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, 0f);
                        Invoke("Jump", 1);
                        jumpHB.enabled = true;

                    }

                    else if (attacking)
                    {
                        action = State.Attacking;
                        anim.SetBool("isMoving", false);
                        anim.SetBool("isAttacking", attacking);
                        theRigidbody.velocity = new Vector2(0f, 0f);
                        Invoke("Attack", 1);
                        punchHB.enabled = true;
                        if (onAttack != null)
                            onAttack(this);
                    }

                    else
                    {
                        if (inputX != 0)
                            anim.SetBool("isMoving", true);
                        else
                            anim.SetBool("isMoving", false);
                        theRigidbody.velocity = new Vector2(inputX * walkSpeed, theRigidbody.velocity.y);
                    }

                    sprRen.flipX = FaceDirection(sprRen.flipX, theRigidbody.velocity.x);
                        
                    break;
                }

            case State.Jumping:
                {
                    bool falling = (theRigidbody.velocity.y < 0) ? true : false;
                    if (falling)
                    {
                        action = State.Falling;
                        anim.SetBool("isFalling", true);
                        anim.SetBool("isJumping", false);
                        jumpHB.enabled = false;
                    }
                    break;
                }

            case State.Falling:
                {
                    theRigidbody.velocity = new Vector2(inputX * walkSpeed, theRigidbody.velocity.y);
                    bool grounded = (theRigidbody.velocity.y == 0);
                    if (grounded)
                    {
                        action = State.Walking;
                        anim.SetBool("isFalling", false);
                    }
                    break;
                }

            case State.Attacking:
                theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, 0f);
                break;
        }
    }

    private void Attack()
    {
        if (sprRen.flipX)
            punchHB.offset = new Vector2(-Mathf.Abs(punchHB.offset.x), punchHB.offset.y);
        else
            punchHB.offset = new Vector2(Mathf.Abs(punchHB.offset.x), punchHB.offset.y);
        //punchHB.enabled = true;
        float direction = (sprRen.flipX) ? -1.0f : 1.0f;
        theRigidbody.AddForce(new Vector2(direction * punchPower * 2, 0));
        anim.SetBool("isIdle", true);
        anim.SetBool("isAttacking", false);
        Invoke("Idle", 0.5f);
        
    }

    private void Idle()
    {
        theRigidbody.velocity = new Vector2(0f, 0f);
        action = State.Walking;
        punchHB.enabled = false;
    }

    private void Jump()
    {
        theRigidbody.AddForce(new Vector2(0, jumpPower));
    }

    private bool FaceDirection(bool flip, float direction)
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
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground") && (action != State.Walking && action != State.Falling))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Building") && (action != State.Walking))
        {
            collision.gameObject.GetComponent<BuildingControl>().Explode();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster") && (action != State.Walking))
        {
            collision.gameObject.GetComponent<MonsterMovement>().Death();
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("ChangeScene"))
        {
            SceneManager.LoadScene("Level2", LoadSceneMode.Single);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Monster") && (action != State.Walking))
        //    collision.gameObject.GetComponent<MonsterMovement>().death();
    }

}
