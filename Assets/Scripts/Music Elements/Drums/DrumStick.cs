using UnityEngine;

public class DrumStick : MonoBehaviour
{
    internal bool interactable;
    OVRGrabbable grabbable;
    public Vector3 previousPos { get; private set; }
    private void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
    }

    public OVRGrabber getGrabber()
    {
        if (grabbable.isGrabbed)
        {
            // use this to trigger vibration 
            return grabbable.grabbedBy;
        }
        else return null;
    }
    private void LateUpdate()
    {
        previousPos = transform.position;
    }
}
