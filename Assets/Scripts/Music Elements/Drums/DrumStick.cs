using UnityEngine;

public class DrumStick : MonoBehaviour
{
    OVRInput.Controller usedController = OVRInput.Controller.None;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogError("Drumstick Collision");
        if (collision.transform.name == GameManager.Instance.LeftHandControllerRoot.transform.name)
        {
            usedController = OVRInput.Controller.LTouch;
            Debug.LogError("LEFT");
        }
        else if (collision.transform.name == GameManager.Instance.RightHandControllerRoot.transform.name)
        {
            usedController = OVRInput.Controller.RTouch;
            Debug.LogError("RIGHT");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.name == GameManager.Instance.LeftHandControllerRoot.transform.name)
        {
            Debug.LogError("NO LEFT");
            usedController = OVRInput.Controller.None;
        }
        else if (collision.transform.name == GameManager.Instance.RightHandControllerRoot.transform.name)
        {
            Debug.LogError("NO RIGHT");
            usedController = OVRInput.Controller.None;
        }
    }
    public OVRInput.Controller GetGrabber()
    {
        return usedController;
    }
}
