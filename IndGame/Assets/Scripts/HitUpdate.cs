using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitUpdate : MonoBehaviour {

    private Text scoreText;
    public static int hit;

    // Use this for initialization
    void Start () {
        scoreText = GetComponent<Text>();
        GameObject.Find("Player").GetComponent<PlayerHit>().onHit += IncrementHit;
        hit = PlayerPrefs.GetInt("hit");
        scoreText.text = "Hit: " + hit;
    }
	
	// Update is called once per frame
    public void IncrementHit(PlayerHit enem)
    {
        hit++;
        PlayerPrefs.SetInt("hit", hit);
        scoreText.text = "Hit: " + hit;
    }

}
