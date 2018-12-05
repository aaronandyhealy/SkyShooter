using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour {

    public GameObject EnemyBulletGO;
    public GameObject enemyFirePoint;
    private float timer;
    private float time;
    private int waitingTime = 3;
    public int scoreValue = 1;               // The amount added to the player's score when the enemy dies.

    private float speed;
    private int direction;
    private float maxDist;
    private float minDist;

    System.Random random = new System.Random(); //Used for random number generation
 



    // Use this for initialization
    void Start () {

        speed = random.Next(1, 5);
        direction = -1;
        maxDist = 9;
        minDist = -9;
        time = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        
        switch (direction)
        {
            case -1:
                // Moving Left
                if (transform.position.x > minDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    direction = 1;
                }
                break;
            case 1:
                //Moving Right
                if (transform.position.x < maxDist)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
                }
                else
                {
                    direction = -1;
                }
                break;
        }

        timer += Time.deltaTime;

        if (time > 20)
        {
            waitingTime = 2;
        }else if(time > 30)
        {
            waitingTime = 1;
        }

        if (timer > waitingTime)
        {
            GameObject bullet = (GameObject)Instantiate(EnemyBulletGO);
            bullet.transform.position = enemyFirePoint.transform.position;

            timer = 0;
        }


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("PlayerBullet"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            GameControl.score += scoreValue;
        }

    }
}
