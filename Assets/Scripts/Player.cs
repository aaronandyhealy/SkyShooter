using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 5f;
    private bool isDead = false;
    private Rigidbody2D rb2d;

    public  GameObject PlayerBulletGO;
    public  GameObject firePoint;
    
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update() {

       

        if (Input.GetKeyDown("space"))
        {
            GameObject bullet = (GameObject)Instantiate(PlayerBulletGO);
            bullet.transform.position = firePoint.transform.position;
        }


        if (isDead == false)
        {

            // What is the player doing with the controls?
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"), 0);

            // Update the ships position each frame
            transform.position += move
                * speed * Time.deltaTime;
        }
	}

    private void OnCollisionEnter2D()
    {
        isDead = true;
        GameControl.instance.Crash();
        
    }


}
