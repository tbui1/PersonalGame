using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public bool disappear;
    public float speed;
    public float high;
    public float low;
	// Use this for initialization
	void Start () {
        Invoke("Disappear", 3.5f);
	}

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            if (disappear)
                Destroy(gameObject);
    }

    public void SetDirection(int dir)
    {
        float p = Random.value;
        Debug.Log(dir * speed);
        if (p <= 0.5)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(dir * speed, 0f);
            
        }
        else
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(dir * low, dir * high), 0f));
            
    }

    void Disappear()
    {
        if (gameObject != null)
            Destroy(gameObject);
    }
}
