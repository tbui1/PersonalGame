using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {
    public float xOff;
    public float yOff;
    public GameObject attack;
    private Transform thisGO;
    private Rigidbody2D theRigidbody;
    private SpriteRenderer sprRen;
    private bool attacking;

    // Use this for initialization
    void Start () {
        theRigidbody = GetComponent<Rigidbody2D>();
        thisGO = gameObject.transform;
        sprRen = GetComponent<SpriteRenderer>();
        attacking = false;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!attacking && collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            attacking = true;
            if (collision.transform.position.x < gameObject.transform.position.x)
            {
                spawnMonsterAttackLeft();
            }
            else
            {
                spawnMonsterAttackRight();
            }
            Invoke("Normal", 1f);
        }
    }

    private void spawnMonsterAttackLeft()
    {
        GameObject bullet = Instantiate(attack, new Vector3(thisGO.position.x - xOff, thisGO.position.y + yOff, 0f), Quaternion.identity);
        BulletMovement mv = bullet.GetComponent<BulletMovement>();
        mv.SetDirection(-1);
    }

    private void spawnMonsterAttackRight()
    {
        GameObject bullet = Instantiate(attack, new Vector3(thisGO.position.x + xOff, thisGO.position.y + yOff, 0f), Quaternion.identity);
        BulletMovement mv = bullet.GetComponent<BulletMovement>();
        mv.SetDirection(1);
    }



    void Normal()
    {
        attacking = false;
    }
}
