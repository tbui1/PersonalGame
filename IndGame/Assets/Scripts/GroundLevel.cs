using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLevel : MonoBehaviour {

    public GameObject groundPrefab;
	// Use this for initialization
	void Start () {
		for (int i = -70; i <= 175; i++)
        {
            for (int j = -5; j > -15; j--)
                Instantiate(groundPrefab, new Vector3(i, j,0), Quaternion.identity);
        }
	}
	// Update is called once per frame
}
