﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {


    public GameObject EnemyBulletGO;
    public GameObject enemyFirePoint;
    float timer;
    private int waitingTime = 3;






    float speed;
    private Vector3 initialPosition;
    private int direction;
    private float maxDist;
    private float minDist;

    // Use this for initialization
    void Start () {
        speed = 2f;
        initialPosition = transform.position;
        direction = -1;
        maxDist += transform.position.x;
        minDist -= transform.position.x;
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
        if (timer > waitingTime)
        {
            GameObject bullet = (GameObject)Instantiate(EnemyBulletGO);
            bullet.transform.position = enemyFirePoint.transform.position;

            timer = 0;
        }
	}
}
