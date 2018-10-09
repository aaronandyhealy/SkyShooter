using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public static GameControl instance;
    public float scrollSpeed = -1.5f;
    public GameObject gameOverText;
    public bool gameOver = false; 

    // Use this for initialization
    void Awake () {
		
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Crash()
    {
        gameOverText.SetActive(true);
        gameOver = true;
    }
}
