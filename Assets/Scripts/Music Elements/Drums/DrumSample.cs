using UnityEngine;
using UnityEngine.UI;

public class DrumSample : MonoBehaviour
{
    FMOD.Studio.EventInstance drumHitSFXInstance;
    public FMODUnity.EventReference eventPath;
    public FMODUnity.EventReference eventPathKeyPressed;

    public Text textElement;

    private float impactSpeed;
    
    private void OnCollisionEnter(Collision collision)
    {
        // get the drum stick object if the interaction on the single drum is noticed = interaction
        if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumStick) && drumStick.interactable == true)
        {
            // calculate the impact speed
            impactSpeed = Vector3.Distance(drumStick.previousPos, drumStick.transform.position) / Time.deltaTime;

        // #if UNITY_EDITOR

            // normalize the impact speed to get a range approx. between 0 and 1
            float normalizedImpactSpeed = impactSpeed / 2;
            // limit the min value to 0 and max value to 1 
            float clampImpactSpeed = Mathf.Clamp(normalizedImpactSpeed,0,1);
            // feedback to console
            Debug.Log("You hit the" + transform.name + "with impact speed: " + clampImpactSpeed);

            textElement.text = clampImpactSpeed.ToString();
        
        // #endif

        if(OVRInput.Get(OVRInput.Button.One)) {
            drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathKeyPressed);
            // drumHitSFXInstance.setParameterByName("Pitch", 0.1f);
        }
        else
        {
            drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPath);
            // drumHitSFXInstance.setParameterByName("Pitch", 1.0f);
        }
        drumHitSFXInstance.setParameterByName("Pitch", impactSpeed);
        drumHitSFXInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
        // drumHitSFXInstance.setParameterByName("Pitch", clampImpactSpeed);
        drumHitSFXInstance.start();
        drumHitSFXInstance.release();



        

        }




    }


}
