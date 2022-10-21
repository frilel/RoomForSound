using UnityEngine;

public class GongSample : MonoBehaviour
{
    FMOD.Studio.EventInstance GongHitSFXInstance;
    public FMODUnity.EventReference eventPathInteractionSoundOne;
    private Transform centerPos;

    private void Start() {
        GongHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundOne);
        GongHitSFXInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        centerPos=transform.GetChild(1).transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumStick))
        {
            Debug.Log(CalculateDistance( collision.GetContact(0).point,centerPos.position));
            //if( collision.GetContact(0).point)
            GongHitSFXInstance.start();
        }
    }

    private void OnDestroy() {
        GongHitSFXInstance.release();
    }
    private float CalculateDistance(Vector3 a, Vector3 b)
    {
        return 10*Vector3.Distance(new Vector3(a.x,a.y,0),new Vector3(b.x,b.y,0));
    }
}
