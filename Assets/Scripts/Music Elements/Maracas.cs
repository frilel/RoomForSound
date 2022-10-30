using System.Collections;
using UnityEngine;
using Oculus.Interaction;
using System;

public class Maracas : MonoBehaviour
{
    // public FMODUnity.EventReference eventPathInteractionSoundOne;
    public FMODUnity.EventReference eventPathInteractionSound;
    FMOD.Studio.EventInstance macaraInstance;
    FMOD.Studio.EventInstance macaraInstance2;
    private float moveSpeed = 0;
    private Vector3 moveSpeedVec = Vector3.zero;
    private Vector3 lastSpeedVec = Vector3.zero;
    private float lastSpeed = 0;
    private float acceleration = 0;
    private float lastAcceleration = 0;
    private float setTriggerVibration = 3f;

    public float setTriggerSound = 1.5f;
    public OVRInput.Controller usedController = OVRInput.Controller.None;
    public Grabbable maracaObj;
    public OVRInput.Controller GetGrabber() => usedController;
    private Guid Latest;
    private VFXController _VFXController;
    private void Start()
    {
        maracaObj = GetComponent<Grabbable>();
        if (_VFXController == null)
            _VFXController = GetComponentInParent<VFXController>();
        macaraInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSound);
        macaraInstance2 = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSound);
        macaraInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        macaraInstance2.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
    private void Update()
    {

        if (maracaObj.isGrabbed)
        {
            //DetectGrabber();
            DetectGrabber();
            moveSpeedVec = GameManager.Instance.Rig.transform.TransformVector(OVRInput.GetLocalControllerVelocity(GetGrabber()));
            moveSpeed = GameManager.Instance.Rig.transform.TransformVector(OVRInput.GetLocalControllerVelocity(GetGrabber())).magnitude;
            acceleration = (moveSpeed - lastSpeed) / Time.deltaTime;
            float clampAcceleration = Mathf.Clamp(acceleration / 10f, 0, 1);
            float clampAccelerationForVolume = Mathf.Sqrt(clampAcceleration);
            clampAcceleration = Mathf.Pow(clampAcceleration, 3);
            Debug.Log(acceleration);
            if (Vector3.Angle(moveSpeedVec, lastSpeedVec) > 90)
            {
                macaraInstance.setParameterByName("Pitch", clampAcceleration);
                macaraInstance.setParameterByName("Volume", clampAccelerationForVolume);
                macaraInstance.start();   
            }
            else if (Mathf.Abs(acceleration) > 15)
            {
                FMOD.Studio.PLAYBACK_STATE state;
                macaraInstance2.getPlaybackState(out state);
                if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    macaraInstance2.setParameterByName("Pitch", clampAcceleration);
                    macaraInstance.setParameterByName("Volume", clampAccelerationForVolume);
                    macaraInstance2.start();
                    _VFXController.triggerVibration(GetGrabber(), 0.1f, 0.1f, 1);
                    //macaraInstance.release();
                }
            }

            lastAcceleration = acceleration;
            lastSpeed = moveSpeed;
            lastSpeedVec = moveSpeedVec;
        }
        else
        {
            macaraInstance.setParameterByName("Pitch", 0);
            macaraInstance.setParameterByName("Volume", 0);
            macaraInstance2.setParameterByName("Pitch", 0);
            macaraInstance.setParameterByName("Volume", 0);
            _VFXController.stopVibration();
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
