using System.Collections;
using UnityEngine;
using Oculus.Interaction;
using System;

public class Maracas : MonoBehaviour
{
    // public FMODUnity.EventReference eventPathInteractionSoundOne;
    public FMODUnity.EventReference eventPathInteractionSound;
    FMOD.Studio.EventInstance macaraInstance;
    private float moveSpeed = 0;
    private float lastSpeed = 0;
    private float acceleration = 0;
    public float setTriggerSound = 1.5f;
    public OVRInput.Controller usedController = OVRInput.Controller.None;
    public Grabbable maracaObj;
    public OVRInput.Controller GetGrabber() => usedController;
    private Guid Latest;
    private void Start()
    {
        maracaObj = GetComponent<Grabbable>();
    }
    private void Update()
    {
        
        if (maracaObj.isGrabbed)
        {
            DetectGrabber();
            moveSpeed = GameManager.Instance.Rig.transform.TransformVector(OVRInput.GetLocalControllerVelocity(GetGrabber())).magnitude;
            if (moveSpeed > 0.001)
            {
                // macaraInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundTwo);
            }
            acceleration = (moveSpeed - lastSpeed) / Time.deltaTime;
            if (acceleration > setTriggerSound)
            {
                macaraInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSound);
                // Debug.Log("maraca make sound");
                macaraInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                macaraInstance.start();
                macaraInstance.release();

            }
            lastSpeed = moveSpeed;

        }

    }
    public void DetectGrabber()
    {
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

    private IEnumerator Debounced()
    {
        // generate a new id and set it as the latest one 
        var guid = Guid.NewGuid();
        Latest = guid;

        // set the denounce duration here
        yield return new WaitForSeconds(0.03f);

        // check if this call is still the latest one
        if (Latest == guid)
        {
            // debounced input handler code here
            macaraInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSound);
            // Debug.Log("maraca make sound");
            macaraInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            macaraInstance.start();
            macaraInstance.release();
        }
    }
}
