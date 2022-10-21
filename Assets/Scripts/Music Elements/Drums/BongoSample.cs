using UnityEngine;

public class BongoSample : MonoBehaviour
{
    FMOD.Studio.EventInstance drumHitSFXInstance;
    public FMODUnity.EventReference eventPathInteractionSoundOne;

    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.tag == "HandAnchor")
        {
            drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundOne);
            drumHitSFXInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            drumHitSFXInstance.start();
            drumHitSFXInstance.release();
        }

    }
}
