using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageUpdate : MonoBehaviour {

    private Text dmgText;
    public static int dmg;

    // Use this for initialization
    void Start()
    {
        dmgText = GetComponent<Text>();
        dmg = PlayerPrefs.GetInt("dmg");
        dmgText.text = "Damage : " + dmg;
    }

    public void IncrementDmg(BuildingControl enem)
    {
        dmg += enem.netWorth;
        enem.onDestroyed -= IncrementDmg;
        PlayerPrefs.SetInt("dmg", dmg);
        dmgText.text = "Damage : " + dmg;
    }
}
