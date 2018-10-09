using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    // Use this for initialization
    private void Start()
    {
        Invoke("Expire", 1.5f);
    }

    // Update is called once per frame
    private void Expire()
    {
        Destroy(gameObject);
    }
}
