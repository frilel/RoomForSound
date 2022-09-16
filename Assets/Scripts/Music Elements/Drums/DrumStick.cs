using UnityEngine;

public class DrumStick : MonoBehaviour
{
    public Vector3 previousPos { get; private set; }

    private void LateUpdate()
    {
        previousPos = transform.position;
    }
}
