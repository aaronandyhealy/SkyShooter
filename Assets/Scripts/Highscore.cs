using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

    public Text Lvl1Highscore;
    public Text Lvl2Highscore;
    public Text Lvl3Highscore;

    // Use this for initialization
    void Start () {
        //Update Text with the players highscore on each level
        Lvl1Highscore.text = ("Level 1 HS: " + PlayerPrefs.GetFloat("LvlOneHighscore"));
        Lvl2Highscore.text = ("Level 2 HS: " + PlayerPrefs.GetFloat("LvlTwoHighscore"));
        Lvl3Highscore.text = ("Level 3 HS: " + PlayerPrefs.GetFloat("LvlThreeHighscore"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
