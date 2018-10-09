using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUp : MonoBehaviour {
    public string nextLevel;
    public bool phit = false;

    private void Start()
    {
        Debug.Log(phit);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        phit = true;
        Debug.Log("hitting");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
    }
}
