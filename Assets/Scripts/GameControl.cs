using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Pose = Thalmic.Myo.Pose;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;   //A reference to our game control script so we can access it statically.
    public static int score;
    public Text scoreText;        //A reference to the UI text component that displays the player's score.
    public GameObject gameOvertext;             //A reference to the object that displays the text which appears when the player dies.
                     //The player's score.
    public bool gameOver = false;               //Is the game over?
    public float scrollSpeed = -1.5f;
    public string loadLevel;


    void Awake()
    {
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;

        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

    
        score = 0;
    }


    void Update()
    {
        scoreText.text = "Score: " + score;
    }

   

    // Use this for initialization
    public void MainMenu()
    {
        SceneManager.LoadScene(loadLevel);
    }

    // Update is called once per frame
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void Crash()
    {
        //Activate the game over text.
        gameOvertext.SetActive(true);
        
        //Set the game to be over.
        gameOver = true;

    }

  
}