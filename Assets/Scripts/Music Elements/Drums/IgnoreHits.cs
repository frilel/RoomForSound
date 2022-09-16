using UnityEngine;

[DefaultExecutionOrder (50)]
public class IgnoreHits : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent<DrumStick>(out DrumStick drumstick))
        {
            drumstick.interactable = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent<DrumStick>(out DrumStick drumstick))
        {
            drumstick.interactable = true;
        }
    }
}
