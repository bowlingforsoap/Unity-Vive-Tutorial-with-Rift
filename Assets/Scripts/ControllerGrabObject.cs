using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
 
    private GameObject collidingObject;
    private GameObject objectInHand;
    	
    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
	}

    private void GrabObject()
    {
        Debug.Log("Grabing Object");

        objectInHand = collidingObject;
        collidingObject = null;

        FixedJoint fixedJoint = AddFixedJoint();
        fixedJoint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private void ReleaseObject()
    {
        Debug.Log("Releasing Object");

        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        objectInHand = null;
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.breakForce = 20000;
        fixedJoint.breakTorque = 20000;
        return fixedJoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    private void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>()) // If we are already holding something or the colliding object has no Rigidbody
        {
            return;
        }

        collidingObject = col.gameObject;
    }

}
