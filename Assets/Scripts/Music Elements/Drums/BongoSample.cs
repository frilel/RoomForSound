using UnityEngine;
using UnityEngine.UI;

public class BongoSample : MonoBehaviour
{
    public Text text;

    FMOD.Studio.EventInstance drumHitSFXInstance;
    public FMODUnity.EventReference eventPathInteractionSoundOne;


    private void OnTriggerEnter(Collider other) {
        
        
        if(other.gameObject.tag == "HandAnchor")
        {
            text.text = other.gameObject.name;

            drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundOne);
            // }
            // drumHitSFXInstance.setParameterByName("Pitch", clampImpactSpeed);
            drumHitSFXInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            drumHitSFXInstance.start();
            drumHitSFXInstance.release();
        }

    }
}
