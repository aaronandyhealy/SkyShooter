using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour {

    public string loadLevel;
    public string loadOne;
    public string loadTwo;
    public string loadThree;

    // Load Level One
    public void PlayLevelOne () {
        SceneManager.LoadScene(loadOne);
	}

    // Load Level Two
    public void PlayLevelTwo()
    {
        SceneManager.LoadScene(loadTwo);
    }

    //Load Level Three
    public void PlayLevelThree()
    {
        SceneManager.LoadScene(loadThree);
    }

    //Load Instructions Page
    public void ViewInstructions()
    {
        SceneManager.LoadScene(loadLevel);
    }

    // Quit Game
    public void QuitGame () {
        Application.Quit();
	}
}
