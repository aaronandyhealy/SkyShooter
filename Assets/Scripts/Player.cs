using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float upForce = 20f;
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
            if(Input.GetMouseButton(0))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
            }
        }
	}

    private void OnCollisionEnter2D()
    {
        isDead = true;
        GameControl.instance.Crash();
        
    }


}
