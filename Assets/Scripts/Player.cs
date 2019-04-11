using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

// Orient the object to match that of the Myo armband.
// Compensate for initial yaw (orientation about the gravity vector) and roll (orientation about
// the wearer's arm) by allowing the user to set a reference orientation.
// Making the fingers spread pose or pressing the 'r' key resets the reference orientation.
public class Player : MonoBehaviour {

    public float speed = 5f;
    private bool isDead = false;

    public AudioClip playerFire;
    public GameObject PlayerBulletGO;
    public GameObject firePoint;

    private int health = 3;
    public GameObject heartOne;
    public GameObject heartTwo;
    public GameObject heartThree;

    public bool isPoseCheckEnabled = true;
    public GameObject myoGameObject;

    Pose lastMyoPose;
    ThalmicMyo myo;
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.


    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.

    // Use this for initialization
    void Start()
    {
        myoGameObject = GameObject.FindGameObjectWithTag("myo");
        myo = myoGameObject.GetComponent<ThalmicMyo>();
    }



    // Update is called once per frame.
    void Update()
    {
        
            lastMyoPose = myo.pose;
            MyoAction();

    }

    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction(ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard)
        {
            myo.Unlock(UnlockType.Timed);
        }

        myo.NotifyUserAction();
    }

    void MyoAction()
    {
        if (isDead == false)
        {
            switch (myo.pose)
            {

                case Pose.WaveOut:
                    if (transform.position.x < 4)
                    {
                        transform.position += Vector3.right * speed * Time.deltaTime;
                    }
                    break;
                case Pose.Fist:
                    Fire();
                    lastMyoPose = Pose.Rest;
                    break;
                case Pose.WaveIn:
                    if (transform.position.x > -4)
                    {
                        transform.position -= Vector3.right * speed * Time.deltaTime;
                    }
                    break;
            }
        }
        else
        {
            switch (myo.pose)
            {
                case Pose.DoubleTap:
                    GameControl.instance.Restart();
                    break;
                case Pose.Fist:
                    GameControl.instance.MainMenu();
                    break;
            }
        }
    }


    //Fire bullet and play audio clip
    public void Fire()
    {
        
            GameObject bullet = (GameObject)Instantiate(PlayerBulletGO);
            bullet.transform.position = firePoint.transform.position;
            AudioSource.PlayClipAtPoint(playerFire, transform.position);
            
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(coll.gameObject);
        if(health == 2)
        {
            health = 1;
            Destroy(heartTwo);
        }
        else if (health == 3)
        {
            health = 2;
            Destroy(heartOne);
        }
        else
        {
            Destroy(heartThree);
            GameOver();
        }

    }

    private void GameOver()
    {
        isDead = true;
        GameControl.instance.Crash();
        
    }


}
