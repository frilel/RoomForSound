using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maracas : MonoBehaviour
{
    public FMODUnity.EventReference eventPathInteractionSoundOne;
    public FMODUnity.EventReference eventPathInteractionSoundTwo;

    private float moveSpeed = 0;
    private float lastSpeed = 0;
    public OVRInput.Controller usedController = OVRInput.Controller.None;
    
    public OVRInput.Controller GetGrabber() => usedController;

    private void Update()
    {
        DetectGrabber();
        if (usedController != OVRInput.Controller.None)
        {
            moveSpeed = GameManager.Instance.Rig.transform.TransformVector(OVRInput.GetLocalControllerVelocity(usedController)).magnitude;
            if (moveSpeed > 1)
            {
                FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundTwo);
                Debug.Log(moveSpeed);
            }
            
            if (moveSpeed - lastSpeed > 2)
            {
                FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundOne);
                Debug.Log(moveSpeed - lastSpeed);
            }
            lastSpeed = moveSpeed;
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
