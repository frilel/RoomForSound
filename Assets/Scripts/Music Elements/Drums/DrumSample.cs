using UnityEngine;

public class DrumSample : MonoBehaviour
{
    public FMODUnity.EventReference _eventPath;
    [SerializeField] VFXController _VFXController;
    private float impactSpeed;

    private void Start()
    {
        _VFXController = GetComponentInParent<VFXController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumStick) && drumStick.interactable == true)
        {
            impactSpeed = Vector3.Distance(drumStick.previousPos, drumStick.transform.position) / Time.deltaTime;
#if UNITY_EDITOR
            Debug.Log("You hit the" + transform.name + "with impact speed: " + impactSpeed);
#endif

        }
        FMODUnity.RuntimeManager.PlayOneShot(_eventPath, transform.position);
        _VFXController.triggerOne(collision.transform);
        if (drumStick)
        {
            _VFXController.triggerVibration(drumStick.getGrabber(), 0.1f, 0.1f, 1);

        }
        //_VFXController.triggerOne(collision.transform, collision.collider.GetComponent<DrumStick>().getGrabber());

    }


}
