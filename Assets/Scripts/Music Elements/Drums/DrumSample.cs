using UnityEngine;
using UnityEngine.UI;

public class DrumSample : MonoBehaviour
{

    FMOD.Studio.EventInstance drumHitSFXInstance;
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
                impactSpeed = collision.collider.attachedRigidbody.velocity.magnitude;
            }

            //DebugInVR.Instance.text.text = $"impactSpeed: {impactSpeed}";

            // normalize the impact speed to get a range approx. between 0 and 1
            //x normalized = (x / x minimum) / (x maximum / x minimum)
            float normalizedImpactSpeed = impactSpeed / 3f;

            // limit the min value to 0 and max value to 1 
            float clampImpactSpeed = Mathf.Clamp(normalizedImpactSpeed, 0, 1);
            // feedback to console
            Debug.Log($"You hit the {transform.name} with impact speed: {clampImpactSpeed}. Using {drumStick.GetGrabber()}");

            // ImpactSpeedText.text = impactSpeed.ToString();


            if(OVRInput.Get(OVRInput.Button.One))
            {
                drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundTwo);
            } 
            else if(OVRInput.Get(OVRInput.Button.Two))
            {
                drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundThree);
            } 
            else 
            {
                drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundOne);
            }
            drumHitSFXInstance.setParameterByName("Pitch", clampImpactSpeed);
            drumHitSFXInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            drumHitSFXInstance.start();
            drumHitSFXInstance.release();

            if (_VFXController != null)
            {
                _VFXController.triggerOne(collision.transform);
                _VFXController.triggerVibration(drumStick.GetGrabber(), 0.1f, 0.1f, 1);
            }
            if (drumStick != null)
            {
                //_VFXController.triggerVibration(drumStick.getGrabber(), 0.1f, 0.1f, 1);
            }
        }

        //FMODUnity.RuntimeManager.PlayOneShot(_eventPath, transform.position);

    }
}
