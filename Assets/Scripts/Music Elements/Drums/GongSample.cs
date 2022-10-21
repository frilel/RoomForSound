using UnityEngine;

public class GongSample : MonoBehaviour
{
    FMOD.Studio.EventInstance GongHitSFXInstance;
    public FMODUnity.EventReference eventPathInteractionSoundOne;

    private void Start() {
        GongHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundOne);
        GongHitSFXInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumStick))
        {
            GongHitSFXInstance.start();
        }
    }

    private void OnDestroy() {
        GongHitSFXInstance.release();
    }
}
