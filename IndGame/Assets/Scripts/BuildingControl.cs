using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControl : MonoBehaviour {

    public delegate void OnDestroyed(BuildingControl mm);
    public event OnDestroyed onDestroyed;


    public int netWorth;
    public GameObject explosion;
	// Use this for initialization
	void Start () {
        //onDestroyed += GameObject.Find("Damage").GetComponent<DamageUpdate>().IncrementDmg;
    }

    // Update is called once per frame
    public void Explode()
    {
        if (onDestroyed != null)
            onDestroyed(this);
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
