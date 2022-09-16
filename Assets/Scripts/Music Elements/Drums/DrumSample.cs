using UnityEngine;

public class DrumSample : MonoBehaviour
{
    public FMODUnity.EventReference _eventPath;

    private float impactSpeed;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumStick))
        {
            impactSpeed = Vector3.Distance(drumStick.previousPos, drumStick.transform.position) / Time.deltaTime;
#if UNITY_EDITOR
            Debug.Log("You hit the" + transform.name + "with impact speed: " + impactSpeed);
#endif
        }

        FMODUnity.RuntimeManager.PlayOneShot(_eventPath, transform.position);
    }


}
