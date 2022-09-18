using UnityEngine;

[DefaultExecutionOrder (50)]
public class IgnoreHits : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<DrumStick>(out DrumStick drumstick))
        {
            drumstick.interactable = false;
            //Debug.Log("Test Enter");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumstick))
        {
            drumstick.interactable = true;
            //Debug.Log("Test Exit");
        }
    }
}
