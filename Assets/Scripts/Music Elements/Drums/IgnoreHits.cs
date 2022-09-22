using UnityEngine;

[DefaultExecutionOrder (50)]
public class IgnoreHits : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<DrumStick>(out DrumStick drumstick))
        {
            //Debug.Log("Test Enter");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.TryGetComponent<DrumStick>(out DrumStick drumstick))
        {
            //Debug.Log("Test Exit");
        }
    }
}
