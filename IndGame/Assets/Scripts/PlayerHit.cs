using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour {
    public delegate void OnHit(PlayerHit hit);
    public event OnHit onHit;

    private bool hit;
    private SpriteRenderer rend;
    // Use this for initialization
    void Start () {
        rend = GetComponent<SpriteRenderer>();
        hit = false;
	}

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hit && other.gameObject.layer == LayerMask.NameToLayer("MonsterAttack"))
        {
            hit = true;
            StartCoroutine(HitAndRun());
            
        }
    }

    IEnumerator HitAndRun()
    {
        if (onHit != null)
        {
            onHit(this);
        }

        for (float t = 0.25f; t > 0f; t -= 0.1f)
        {
            rend.color = Color.white;
            yield return new WaitForSeconds(t);
            rend.color = new Color(1f, 1f, 1f, 0.3f);
            yield return new WaitForSeconds(t);
        }

        hit = false;
        rend.color = Color.white;
    }
}
