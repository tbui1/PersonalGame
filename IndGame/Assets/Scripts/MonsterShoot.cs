using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShoot : MonoBehaviour {

    public float xOff;
    public float yOff;
    public float sight;
    public GameObject fire;
    private Transform thisGO;
    private Rigidbody2D theRigidbody;
    private SpriteRenderer sprRen;
    private bool attacking;
    private LayerMask nonTarget;
    void Start () {
        theRigidbody = GetComponent<Rigidbody2D>();
        thisGO = gameObject.transform;
        sprRen = GetComponent<SpriteRenderer>();
        attacking = false;
        nonTarget = 1 << LayerMask.NameToLayer("Monster");
	}


    private void Update()
    {
        if (!attacking)
        {
            Vector2 dir = GetFacing();
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(thisGO.transform.position.x, thisGO.transform.position.y- 0.5f) + dir, dir, sight, LayerMask.GetMask("Player"));
            if (hit.collider != null)
            {
                attacking = true;
                Debug.Log(hit.distance);
                Debug.Log("Hitting");
                if (dir.x < 0)
                {
                    spawnMonsterAttackLeft();
                }
                else
                {
                    spawnMonsterAttackRight();
                }
                Invoke("Normal", 1);
            }
        }
    }

    private void spawnMonsterAttackLeft()
    {
        GameObject bullet = Instantiate(fire, new Vector3(thisGO.position.x - xOff, thisGO.position.y + yOff, 0f), Quaternion.identity);
        BulletMovement mv = bullet.GetComponent<BulletMovement>();
        mv.SetDirection(-1);
    }

    private void spawnMonsterAttackRight()
    {
        GameObject bullet = Instantiate(fire, new Vector3(thisGO.position.x + xOff, thisGO.position.y + yOff, 0f), Quaternion.identity);
        BulletMovement mv = bullet.GetComponent<BulletMovement>();
        mv.SetDirection(1);
    }


    private Vector2 GetFacing()
    {
        if (sprRen.flipX)
            return new Vector2(-xOff * 2, 0f);
        else
            return new Vector2(xOff * 2, 0f);
        
    }

    void Normal()
    {
        attacking = false;
    }

}
