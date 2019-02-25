using System.Collections;
using System.Collections.Generic;
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


    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;

    // A rotation that compensates for the Myo armband's orientation parallel to the ground, i.e. yaw.
    // Once set, the direction the Myo armband is facing becomes "forward" within the program.
    // Set by making the fingers spread pose or pressing "r".
    private Quaternion _antiYaw = Quaternion.identity;

    // A reference angle representing how the armband is rotated about the wearer's arm, i.e. roll.
    // Set by making the fingers spread pose or pressing "r".
    private float _referenceRoll = 0.0f;

    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;



    // Update is called once per frame.
    void Update()
    {
        if (isDead == false)
        {
            // Access the ThalmicMyo component attached to the Myo object.
            ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

            // Vibrate the Myo armband when a fist is made.
            if (thalmicMyo.pose == Pose.WaveOut)
            {
                if (transform.position.x < 8)
                {
                    transform.position += Vector3.right * speed * Time.deltaTime;
                }

                ExtendUnlockAndNotifyUserAction(thalmicMyo);

                // Change material when wave in, wave out or double tap poses are made.
            }
            else if (thalmicMyo.pose == Pose.WaveIn)
            {
                if (transform.position.x > -8)
                {
                    transform.position -= Vector3.right * speed * Time.deltaTime;
                }

                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
            else if (thalmicMyo.pose == Pose.Fist)
            {
                Fire();

                ExtendUnlockAndNotifyUserAction(thalmicMyo);
            }
        }
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
        Destroy(gameObject);
    }


}
