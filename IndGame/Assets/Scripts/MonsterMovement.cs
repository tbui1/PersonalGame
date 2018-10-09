using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Movement { Left, Right, Attacking }

public class MonsterMovement : MonoBehaviour {
    public float speed;
    public GameObject explosionPrefab;
    public float groundRadius = 0.1f;
    public Transform lg;
    public Transform rg;
    public Transform turnLeft;
    public Transform turnRight;
    public LayerMask groundMask;
    public int score;
    public delegate void OnDeath(MonsterMovement mm);
    public event OnDeath onDeath;
    public LayerMask buildingMask;

    private BoxCollider2D hb;
    private SpriteRenderer sprRen;
    private Animator anim;
    private Rigidbody2D theRigidbody;
    private Movement dir;
    private Movement prev;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        theRigidbody = GetComponent<Rigidbody2D>();
        dir = Movement.Left;
        sprRen = GetComponent<SpriteRenderer>();
        anim.SetBool("isMoving", true);
        hb = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (dir)
        {
            case Movement.Left:
                {
                    sprRen.flipX = true;
                    bool canGo = (Physics2D.OverlapCircle(lg.position, groundRadius, groundMask) || Physics2D.OverlapCircle(lg.position, groundRadius, buildingMask)) && !Physics2D.OverlapCircle(turnRight.position, groundRadius, buildingMask);
                    if (!canGo)
                    {
                        sprRen.flipX = false;
                        dir = Movement.Right;
                    }
                    theRigidbody.velocity = new Vector2(-speed, theRigidbody.velocity.y);
                    break;
                }
            case Movement.Right:
                {
                    sprRen.flipX = false;
                    bool canGo = (Physics2D.OverlapCircle(rg.position, groundRadius, groundMask) || Physics2D.OverlapCircle(rg.position, groundRadius, buildingMask)) && !Physics2D.OverlapCircle(turnLeft.position, groundRadius, buildingMask);
                    if (!canGo)
                    {
                        sprRen.flipX = true;
                        dir = Movement.Left;
                    }
                    theRigidbody.velocity = new Vector2(speed, theRigidbody.velocity.y);
                    break;
                }
            case Movement.Attacking:
                break;
        }
        if (sprRen.flipX)
            hb.offset = new Vector2(-Mathf.Abs(hb.offset.x), hb.offset.y);
        else
            hb.offset = new Vector2(Mathf.Abs(hb.offset.x), hb.offset.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.layer == LayerMask.NameToLayer("Player"))
        {
            if (other.transform.position.x < gameObject.transform.position.x)
            {
                prev = Movement.Left;
                sprRen.flipX = true;
            }
            else
            {
                prev = Movement.Right;
                sprRen.flipX = false;
            }

            dir = Movement.Attacking;
            theRigidbody.velocity = new Vector2(0f, 0f);
            anim.SetBool("isMoving", false);
            anim.SetBool("isAttacking", true);
            Invoke("WalkAgain", 1);
        }
    }

    private void WalkAgain()
    {
        dir = prev;
        anim.SetBool("isAttacking", false);
        anim.SetBool("isMoving", true);
    }

    public void Death()
    {
        if (onDeath != null)
            onDeath(this);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
