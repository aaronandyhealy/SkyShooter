using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour {

    public string levelOne;
    public string levelTwo;
    public string levelThree;
    public string exitMenu;

    // Use this for initialization
    public void PlayLevelOne()
    {
        SceneManager.LoadScene(levelOne);
    }

    public void PlayLevelTwo()
    {
        SceneManager.LoadScene(levelTwo);
    }

    public void PlayLevelThree()
    {
        SceneManager.LoadScene(levelThree);
    }

    // Update is called once per frame
    public void ExitMenu()
    {
        SceneManager.LoadScene(exitMenu);
    }
}
