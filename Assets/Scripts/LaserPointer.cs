using UnityEngine;

public class LaserPointer : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public GameObject laserPrefab;
    public GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

    public Transform cameraRigTransform;
    public GameObject teleportRectilePrefab;
    private GameObject rectile;
    private Transform teleportRectileTransform;
    public Transform headTransform;
    public Vector3 teleportRectileOffset;
    public LayerMask teleportMask;
    private bool shouldTeleport;

    void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;

        rectile = Instantiate(teleportRectilePrefab);
        teleportRectileTransform = rectile.transform;
    }
	
	// Update is called once per frame
	void Update () {
        // Show laser and rectile
        if (Controller.GetTouch(SteamVR_Controller.ButtonMask.Axis0)) // Joystick touch
        {
            RaycastHit hit;
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);

                rectile.SetActive(true);
                teleportRectileTransform.position = hitPoint + teleportRectileOffset;
                shouldTeleport = true;
            } else
            {
                PreventFurtherTeleport();
            }
        }
        else
        {
            PreventFurtherTeleport();
        }

        // Teleport
        //if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport) // Joystick press
        if (Controller.GetHairTriggerDown() && shouldTeleport) // Hair Trigger press (more convenient than pressing the joystick IMHO)
        {
            Teleport();
        }
	}

    private void Teleport()
    {
        PreventFurtherTeleport();
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0f;
        cameraRigTransform.position = hitPoint + difference;
    }
    
    // Prevents teleport in cases, such as when a laser was pointing at the wall, but now it's pointing on the wall.
    private void PreventFurtherTeleport()
    {
        shouldTeleport = false;
        laser.SetActive(false);
        rectile.SetActive(false);
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f); // Place the laser in the middle between the controller and the hitPoint
        laserTransform.LookAt(hitPoint);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
    }
}
