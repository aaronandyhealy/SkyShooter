using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 5f;
    private bool isDead = false;
    
    public AudioClip playerFire;
    public  GameObject PlayerBulletGO;
    public  GameObject firePoint;
    public string level;
    private bool one = false;
    private bool two = false;
    private bool three = false;

    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 30.0f;
    private float maxSwipeTime = 0.5f;
    bool canInvoke = true;


    // Use this for initialization
    void Start () {
        Input.multiTouchEnabled = true;

    }

    // Update is called once per frame
    void Update() {

        if (isDead == false)
        {
            if (Input.touchCount > 0 && Time.timeScale > 0.0f)
            {

                foreach (Touch touch in Input.touches)
                {
                    if ((touch.position.x < Screen.width / 2))
                    {
                        if (touch.phase == TouchPhase.Began)
                    {
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                    }

                    if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
                    {


                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if (canInvoke && isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                        {
                                canInvoke = false;
                                Invoke("invokeMovement", .4f);

                                Vector2 direction = touch.position - fingerStartPos;
                                //Vector2 swipeType = Vector2.zero;
                                int swipeType = -1;
                                if (Mathf.Abs(direction.normalized.x) > 0.9)
                                {

                                    if (Mathf.Sign(direction.x) > 0) swipeType = 0; // swipe right
                                    else swipeType = 1; // swipe left

                                }
                                else if (Mathf.Abs(direction.normalized.y) > 0.9)
                                {
                                    if (Mathf.Sign(direction.y) > 0) swipeType = 2; // swipe up
                                    else swipeType = 3; // swipe down
                                }


                                switch (swipeType)
                                {

                                    case 0: //right
                                        if (transform.position.x < 8)
                                        {
                                            transform.position += Vector3.right * speed * Time.deltaTime;
                                        }
                                        break;


                                    case 1: //left
                                        if (transform.position.x > -8)
                                        {
                                            transform.position -= Vector3.right * speed * Time.deltaTime;
                                        }
                                        break;

                                    case 2: //up
                                        if (transform.position.y < 1.5)
                                        {
                                            transform.position += Vector3.up * speed * Time.deltaTime;
                                        }
                                        break;

                                    case 3: //down
                                        if (transform.position.y > -2.5)
                                        {
                                            transform.position -= Vector3.up * speed * Time.deltaTime;
                                        }
                                        break;


                                }
                            }

                        }

                    }

                    else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        canInvoke = true;
                    }

                }

            }
        }
    }


    public void invokeMovement()
    {

        canInvoke = true;

    }


    //Fire bullet and play audio clip
    public void Fire()
    {

        GameObject bullet = (GameObject)Instantiate(PlayerBulletGO);
        bullet.transform.position = firePoint.transform.position;
        AudioSource.PlayClipAtPoint(playerFire, transform.position);
    }

    private void OnCollisionEnter2D()
    {
        //Check level and dependig on this update highscore
        one = string.Equals(level, "One");
        two = string.Equals(level, "Two");
        three = string.Equals(level, "Three");
        if (one == true)
        {
            if (PlayerPrefs.GetFloat("LvlOneHighscore") < GameControl.score)
            {
                PlayerPrefs.SetFloat("LvlOneHighscore", GameControl.score);
            }
        }
        else if (two == true)
        {
            if (PlayerPrefs.GetFloat("LvlTwoHighscore") < GameControl.score)
            {
                PlayerPrefs.SetFloat("LvlTwoHighscore", GameControl.score);
            }
        }
        else if (three == true)
        {
            if (PlayerPrefs.GetFloat("LvlThreeHighscore") < GameControl.score)
            {
                PlayerPrefs.SetFloat("LvlThreeHighscore", GameControl.score);
            }
        }
        isDead = true;
        GameControl.instance.Crash();
        Destroy(gameObject);

    }


}
