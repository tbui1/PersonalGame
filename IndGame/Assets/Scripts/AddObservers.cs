using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObservers : MonoBehaviour {

    public DamageUpdate dmgUpdate;
	// Use this for initialization
	void Start () {
        
        BuildingControl [] enemies = FindObjectsOfType<BuildingControl>();
        Debug.Log(enemies.Length);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].onDestroyed += dmgUpdate.IncrementDmg;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
