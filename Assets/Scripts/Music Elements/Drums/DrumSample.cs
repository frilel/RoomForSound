using UnityEngine;

public class DrumSample : MonoBehaviour
{
    FMOD.Studio.EventInstance drumHitSFXInstance;
    public FMODUnity.EventReference eventPath;

    private float impactSpeed;
    
    private void OnCollisionEnter(Collision collision)
    {
        // get the drum stick object if the interaction on the single drum is noticed = interaction
        if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumStick))
        {
            // TODO: must somehow figure out which controller hit the drum and replace "OVRInput.Controller.RTouch"
            impactSpeed = GameManager.Instance.ControllerTrackingSpace.transform.TransformVector(
                OVRInput.GetLocalControllerVelocity(drumStick.getGrabber())).magnitude;
            
            DebugInVR.Instance.text.text = $"impactSpeed: {impactSpeed}";

            // normalize the impact speed to get a range approx. between 0 and 1
            //x normalized = (x – x minimum) / (x maximum – x minimum)
            float normalizedImpactSpeed = impactSpeed / 3f;

            // limit the min value to 0 and max value to 1 
            float clampImpactSpeed = Mathf.Clamp(normalizedImpactSpeed, 0f, 1f);

#if UNITY_EDITOR
            // feedback to console
            Debug.Log("You hit the" + transform.name + "with impact speed: " + clampImpactSpeed);
#endif

            drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
            drumHitSFXInstance.setParameterByName("Pitch", clampImpactSpeed);
            drumHitSFXInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            drumHitSFXInstance.start();
            drumHitSFXInstance.release();

        }
    }

    //private void oncollisionexit(collision collision)
    //{
    //    if (collision.transform.trygetcomponent<drumstick>(out drumstick drumstick))
    //    {
    //        drumstick.interactable = true;
    //        debug.log("exit");
    //    }
    //}

    //private void oncollisionstay(collision collision)
    //{
    //    if (collision.transform.trygetcomponent<drumstick>(out drumstick drumstick))
    //    {
    //        if(drumstick.interactable == true)
    //            drumstick.interactable = false;
    //        //debug.log("stay");
    //    }
    //}
}
