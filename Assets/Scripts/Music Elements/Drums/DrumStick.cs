using UnityEngine;
using Oculus.Interaction;

public class DrumStick : MonoBehaviour
{
    public OVRInput.Controller usedController = OVRInput.Controller.None;
    
    public OVRInput.Controller GetGrabber() => usedController;
    InstructionController instructionController;
    Grabbable grabbable;
    public void Start()
    {
        instructionController = FindObjectOfType<InstructionController>();
        grabbable = GetComponent<Grabbable>();
    }
    public void Update()
    {
        if (grabbable.isGrabbed) {
            instructionController.motionIDFinish[1] = true;
            instructionController.motionIDFinish[3] = true;
        }
    }
    public void DetectGrabber()
    {
        if (usedController != OVRInput.Controller.None)
            return;

        // If both hands are within this overlap check we might save the wrong controller
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 0.1f, Physics.AllLayers, QueryTriggerInteraction.Collide);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].transform.name == GameManager.Instance.LeftHandAnchor.transform.name)
                usedController = OVRInput.Controller.LTouch;
            else if (hitColliders[i].transform.name == GameManager.Instance.RightHandAnchor.transform.name)
                usedController = OVRInput.Controller.RTouch;
        }
    }

    public void RemoveGrabber() => usedController = OVRInput.Controller.None;
}
