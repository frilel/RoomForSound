using UnityEngine;

public class DrumSample : MonoBehaviour
{
    FMOD.Studio.EventInstance drumHitSFXInstance;
    public FMODUnity.EventReference eventPath;
    private float impactSpeed;
    VFXController _VFXController;
    private void Start()
    {
        _VFXController = GetComponentInParent<VFXController>();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        // get the drum stick object if the interaction on the single drum is noticed = interaction
        //if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumStick) && drumStick.interactable == true)
        if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumStick))
        //if(collision.transform.CompareTag("stick"))
        {
            //drumStick=collision.transform.GetComponent<DrumStick>();
            //Debug.Log("ssss");
            // calculate the impact speed
            impactSpeed = Vector3.Distance(drumStick.previousPos, drumStick.transform.position) / Time.deltaTime;

        #if UNITY_EDITOR

            // normalize the impact speed to get a range approx. between 0 and 1
            float normalizedImpactSpeed = impactSpeed / 2;
            // limit the min value to 0 and max value to 1 
            float clampImpactSpeed = Mathf.Clamp(normalizedImpactSpeed,0,1);
            // feedback to console
            Debug.Log("You hit the" + transform.name + "with impact speed: " + clampImpactSpeed);
        
        #endif

        drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
        drumHitSFXInstance.setParameterByName("Pitch", clampImpactSpeed);
        drumHitSFXInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        drumHitSFXInstance.start();
        drumHitSFXInstance.release();
        }
        //FMODUnity.RuntimeManager.PlayOneShot(eventPath, transform.position);
        if(_VFXController != null) 
        {
            _VFXController.triggerOne(collision.transform);
            _VFXController.triggerVibration(OVRInput.Controller.RTouch, 0.1f, 0.1f, 1);
        }
        if (drumStick!=null)
        {
            //_VFXController.triggerVibration(drumStick.getGrabber(), 0.1f, 0.1f, 1);
        }




    }


}
