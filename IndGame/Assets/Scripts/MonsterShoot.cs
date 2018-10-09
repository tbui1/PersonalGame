using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterShoot : MonoBehaviour {

    public float xOff;
    public float yOff;
    public GameObject fire;
    private Transform thisGO;
    private Rigidbody2D theRigidbody;
    void Start () {
        theRigidbody = GetComponent<Rigidbody2D>();
        thisGO = gameObject.transform;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.Log("hitting");
            
            if (collision.gameObject.transform.position.x < thisGO.position.x)
            {
                Invoke("spawnMonsterAttackLeft", 1f);
            }
            else
            {
                Invoke("spawnMonsterAttackRight", 1f);
            }
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //Debug.Log("Shooting");
            Invoke("Normal", 1);
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

    void MonsterAttack()
    {

    }

    void Normal()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

}
