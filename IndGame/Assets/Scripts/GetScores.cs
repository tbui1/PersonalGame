using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetScores : MonoBehaviour {

    public Text tx;

	// Use this for initialization
	void Start () {
        int score = PlayerPrefs.GetInt("score");
        int hit = PlayerPrefs.GetInt("hit");
        int dmg = PlayerPrefs.GetInt("dmg");
        tx.text = "Congrats !!!" + "\n\n" + "You have \ndefeated " + score + " monsters,\n" + "hit " + hit + " times,\n" + "and caused $" + dmg + " worth of damages\n";

        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.DeleteKey("hit");
        PlayerPrefs.DeleteKey("dmg");
	}
	
	// Update is called once per frame

}
