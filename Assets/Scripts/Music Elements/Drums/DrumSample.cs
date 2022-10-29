using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DrumSample : MonoBehaviour
{
    FMOD.Studio.EventInstance drumInstanceOne;
    FMOD.Studio.EventInstance drumInstanceTwo;
    FMOD.Studio.EventInstance drumInstanceThree;
    public FMODUnity.EventReference eventPathInteractionSoundOne;
    public FMODUnity.EventReference eventPathInteractionSoundTwo;
    public FMODUnity.EventReference eventPathInteractionSoundThree;
    Text ImpactSpeedText;

    [SerializeField] private VFXController _VFXController;

    private float impactSpeed;

    private void Start()
    {
        if(_VFXController == null)
            _VFXController = GetComponentInParent<VFXController>();
            // ImpactSpeedText = GameObject.Find("ImpactSpeedText").GetComponent<Text>();

        drumInstanceTwo = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundTwo);
        drumInstanceTwo.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

        drumInstanceThree = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundThree);
        drumInstanceThree.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

        drumInstanceOne = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundOne);
        drumInstanceOne.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    private void OnCollisionEnter(Collision collision)
    {
        // get the drum stick object if the interaction on the single drum is noticed = interaction
        if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumStick))
        {
            if (drumStick.GetGrabber() != OVRInput.Controller.None)
            {
                impactSpeed = GameManager.Instance.Rig.transform.TransformVector(
                    OVRInput.GetLocalControllerVelocity(drumStick.GetGrabber())).magnitude;
            }
            else
            {
                // won't work with drumstick but maybe something else hits the drum?
                impactSpeed = collision.collider.attachedRigidbody.velocity.magnitude;
            }

            // normalize the impact speed to get a range approx. between 0 and 1
            //x normalized = (x / x minimum) / (x maximum / x minimum)
            float normalizedImpactSpeed = impactSpeed / 3f;

            // limit the min value to 0 and max value to 1 
            float clampImpactSpeed = Mathf.Clamp(normalizedImpactSpeed, 0, 1);

            // feedback to console
            //Debug.Log($"You hit the {transform.name} with impact speed: {clampImpactSpeed}. Using {drumStick.GetGrabber()}");

            PlaySFX(clampImpactSpeed);
            PlayVFX(collision.transform, drumStick.GetGrabber());
        }

    }
            if(OVRInput.Get(OVRInput.Button.One))
            {
                drumInstanceTwo.setParameterByName("Pitch", clampImpactSpeed);
                drumInstanceTwo.start();
            } 
            else if(OVRInput.Get(OVRInput.Button.Two))
            {
                drumInstanceThree.setParameterByName("Pitch", clampImpactSpeed);
                drumInstanceThree.start();
            } 
            else 
            {
                drumInstanceOne.setParameterByName("Pitch", clampImpactSpeed);
                drumInstanceOne.start();
            }
            

    /// <summary>
    /// Executed when pressing HiHat pedal.
    /// </summary>
    public void PlayHiHatPedal(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Performed)
        {
            PlaySFX(1);
        }
    }

    public void PlaySFX(float clampImpactSpeed01)
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundTwo);
        }
        else if (OVRInput.Get(OVRInput.Button.Two))
        {
            drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundThree);
        }
        else
        {
            drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundOne);
        }
        drumHitSFXInstance.setParameterByName("Pitch", clampImpactSpeed01);
        drumHitSFXInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        drumHitSFXInstance.start();
        drumHitSFXInstance.release();
    }

        _VFXController.triggerOne(spawnLoc);
        _VFXController.triggerVibration(usedController, 0.1f, 0.1f, 1);
    }
        private void OnDestroy() {
            drumInstanceOne.release();
            drumInstanceTwo.release();
            drumInstanceThree.release();
        }
}
