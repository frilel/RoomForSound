using UnityEngine;

public class GongSample : MonoBehaviour
{
    FMOD.Studio.EventInstance gongOuterInstance;
    FMOD.Studio.EventInstance gongMidInstance;
    FMOD.Studio.EventInstance gongCenterInstance;
    public FMODUnity.EventReference gongOuter;
    public FMODUnity.EventReference gongMid;
    public FMODUnity.EventReference gongCenter;
    private Transform centerPos;

    private void Start() {
        gongOuterInstance = FMODUnity.RuntimeManager.CreateInstance(gongOuter);
        gongMidInstance = FMODUnity.RuntimeManager.CreateInstance(gongMid);
        gongCenterInstance = FMODUnity.RuntimeManager.CreateInstance(gongCenter);

        gongOuterInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        gongMidInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        gongCenterInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

        centerPos=transform.GetChild(1).transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumStick))
        {
            float hitMark = CalculateDistance(collision.GetContact(0).point,centerPos.position);
            if(hitMark <= 1.3f)
            {
                gongCenterInstance.start();
            } else if(hitMark > 1.3f && hitMark < 2.6f) 
            {
                gongMidInstance.start();
            } else if(hitMark > 2.6f && hitMark < 5f)
            {
                gongOuterInstance.start();
            }
        }
    }

    private void OnDestroy() {
        gongCenterInstance.release();
        gongMidInstance.release();
        gongOuterInstance.release();
    }
    private float CalculateDistance(Vector3 a, Vector3 b)
    {
        return 10*Vector3.Distance(new Vector3(a.x,a.y,0),new Vector3(b.x,b.y,0));
    }
}
