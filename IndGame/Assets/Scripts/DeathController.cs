using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour {

    public float delay;
    // Use this for initialization
    void Start () {
        Invoke("done", delay);
	}
	
    void done()
    {
        Destroy(gameObject);
    }
	// Update is called once per frame

}
