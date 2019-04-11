using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;
public class MainMenu : MonoBehaviour {

   
    //Items to choose when poses are made.
    public string fingersSpread;
    public string doubleTap;
    public bool isPoseCheckEnabled = true;
    public GameObject myoGameObject;

    Pose lastMyoPose;
    ThalmicMyo myo;

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


    // Access the ThalmicMyo component attached to the Myo game object.
    /*   ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo>();

       // Check if the pose has changed since last update.
       // The ThalmicMyo component of a Myo game object has a pose property that is set to the
       // currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
       // detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
       // is not on a user's arm, pose will be set to Pose.Unknown.
       if (thalmicMyo.pose != _lastPose)
       {
           _lastPose = thalmicMyo.pose;

           // Vibrate the Myo armband when a fist is made.
           if (thalmicMyo.pose == Pose.Fist)
           {
               thalmicMyo.Vibrate(VibrationType.Medium);

               ExtendUnlockAndNotifyUserAction(thalmicMyo);

               // Change material when wave in, wave out or double tap poses are made.
           }
           else if (thalmicMyo.pose == Pose.FingersSpread)
           {
               SceneManager.LoadScene(fingersSpread);

               ExtendUnlockAndNotifyUserAction(thalmicMyo);
           }
           else if (thalmicMyo.pose == Pose.Fist)
           {
               Application.Quit();

               ExtendUnlockAndNotifyUserAction(thalmicMyo);
           }
           else if (thalmicMyo.pose == Pose.DoubleTap)
           {
               SceneManager.LoadScene(doubleTap);

               ExtendUnlockAndNotifyUserAction(thalmicMyo);
           }
       } */


    public void a()
    {
        SceneManager.LoadScene(fingersSpread);
    }

    void MyoAction()
    {
        switch (myo.pose)
        {
            case Pose.FingersSpread:
                Debug.Log("fs");
                SceneManager.LoadScene(fingersSpread);
                break;
            case Pose.Fist:
                Debug.Log("q");
                Application.Quit();
                break;
            case Pose.DoubleTap:
                Debug.Log("dt");
                SceneManager.LoadScene(doubleTap);
                break;
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
}
