using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour {

    private Text scoreText;
    public static int score;

    // Use this for initialization
    void Start()
    {
        scoreText = GetComponent<Text>();
        score = PlayerPrefs.GetInt("score");
        scoreText.text = "Score: " + score;
    }

    public void IncrementScore(MonsterMovement enem)
    {
        score++;
        Debug.Log(score);
        enem.onDeath -= IncrementScore;
        PlayerPrefs.SetInt("score", score);

        scoreText.text = "Score: " + score;
    }

}
