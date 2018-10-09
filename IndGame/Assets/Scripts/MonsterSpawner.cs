using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public int curLevel;
    public AudioController ac;
    public GameObject mon1;
    public GameObject mon2;
    public GameObject mon3;
    public GameObject mon4;
    public GameObject mon5;

	// Use this for initialization
	void Start () {
        if (curLevel == 1)
            Gen(0.35f, 0.7f, 0.85f, 95f);
        else if (curLevel == 2)
            Gen(0.25f, 0.5f, 0.70f, 0.80f);
        else if (curLevel == 3)
            Gen(0.1f, 0.2f, 0.5f, 0.75f);
    }
	
    void Gen(float fst, float snd, float third, float fourth)
    {
        GameObject temp;
        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = new Vector3(gameObject.transform.position.x + i, gameObject.transform.position.y, gameObject.transform.position.z);
            float p = Random.value;
            if (p <= fst)
                temp = Instantiate(mon1, pos, Quaternion.identity);
            else if (p < snd)
                temp = Instantiate(mon2, pos, Quaternion.identity);
            else if (p < third)
                temp = Instantiate(mon3, pos, Quaternion.identity);
            else if (p < fourth)
                temp = Instantiate(mon4, pos, Quaternion.identity);
            else
                temp = Instantiate(mon5, pos, Quaternion.identity);

            ScoreUpdate sc = GameObject.Find("Score").GetComponent<ScoreUpdate>();
            MonsterMovement mm = temp.GetComponent<MonsterMovement>();
            mm.onDeath += sc.IncrementScore;
            mm.onDeath += ac.PlaySplash;
            
        }
    }
    // Update is called once per frame
}
