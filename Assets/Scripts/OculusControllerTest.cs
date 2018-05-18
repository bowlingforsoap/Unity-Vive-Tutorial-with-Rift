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
