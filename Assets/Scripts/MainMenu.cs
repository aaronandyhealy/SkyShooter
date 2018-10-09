using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string loadLevel;

	// Use this for initialization
	public void PlayGame () {
        SceneManager.LoadScene(loadLevel);
	}
	
	// Update is called once per frame
	public void QuitGame () {
        Application.Quit();
	}
}
