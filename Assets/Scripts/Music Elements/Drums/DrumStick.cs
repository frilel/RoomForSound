using UnityEngine;

public class DrumStick : MonoBehaviour
{
    internal bool interactable;

    public Vector3 previousPos { get; private set; }

    /*public OVRInput.Controller getGrabber()
    {
        if (grabbable.isGrabbed)
        {
            // use this to trigger vibration 
            return grabbable.grabbedBy.GetController();
        }
        else return OVRInput.Controller.RTouch;
    }
    private void LateUpdate()
    {
        previousPos = transform.position;
    }*/
}
