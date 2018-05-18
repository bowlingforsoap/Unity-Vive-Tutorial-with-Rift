using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusControllerTest : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update () {
        if (Controller.GetTouch(SteamVR_Controller.ButtonMask.Axis0)) // Capacitive touch on Joystick on Rift
        {
            Debug.Log(gameObject.name + "Capacitive Touch Axis " + 0);
        }

        //if (Controller.GetTouch(SteamVR_Controller.ButtonMask.Axis1))
        //{
        //    Debug.Log(gameObject.name + "Capacitive Touch Axis " + 1);
        //}

        //if (Controller.GetTouch(SteamVR_Controller.ButtonMask.Axis2)) // Somethign with Grip pressing on Rift
        //{
        //    Debug.Log(gameObject.name + "Capacitive Touch Axis " + 2);
        //}

        //if (Controller.GetTouch(SteamVR_Controller.ButtonMask.Axis3))
        //{
        //    Debug.Log(gameObject.name + "Capacitive Touch Axis " + 3);
        //}

        //if (Controller.GetTouch(SteamVR_Controller.ButtonMask.Axis4))
        //{
        //    Debug.Log(gameObject.name + "Capacitive Touch Axis " + 4);
        //}

        if (Controller.GetAxis() != Vector2.zero) // Touchpad for Vive; Joystick for Rift
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }	

        if (Controller.GetHairTriggerDown()) // true, when was pressed on prev ?frame
        {
            Debug.Log(gameObject.name + "Trigger Full Press");
        }

        if (Controller.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + "Trigger Release");
        }

        //if (Controller.GetHairTrigger()) // true when pressed
        //{
        //    Debug.Log(gameObject.name + "Trigger Press " + Controller.GetHairTrigger());
        //}

        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) // true, when was gripped on the prev ?frame; side buttons for Vive; Grip button for Rift
        {
            Debug.Log(gameObject.name + "Grip Full Press");
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + "Grip Release");
        }

        //if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip)) // true when pressed
        //{
        //    Debug.Log(gameObject.name + "Grip Press " + Controller.GetPress(SteamVR_Controller.ButtonMask.Grip));
        //}
    }
}
