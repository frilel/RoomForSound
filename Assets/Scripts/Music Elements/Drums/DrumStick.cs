using UnityEngine;

public class DrumStick : MonoBehaviour
{
    OVRInput.Controller usedController = OVRInput.Controller.None;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == GameManager.Instance.LeftHandControllerRoot.transform.name)
        {
            usedController = OVRInput.Controller.LTouch;
        }
        else if (collision.transform.name == GameManager.Instance.RightHandControllerRoot.transform.name)
        {
            usedController = OVRInput.Controller.RTouch;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.name == GameManager.Instance.LeftHandControllerRoot.transform.name)
        {
            usedController = OVRInput.Controller.None;
        }
        else if (collision.transform.name == GameManager.Instance.RightHandControllerRoot.transform.name)
        {
            usedController = OVRInput.Controller.None;
        }
    }
    public OVRInput.Controller GetGrabber()
    {
        return usedController;
    }
}
