using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {
    public string loadLevel;

    // Return To Main Menu
    public void ReturnToMenu () {
        SceneManager.LoadScene(loadLevel);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
