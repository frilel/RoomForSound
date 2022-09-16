using UnityEngine;

public class DrumStick : MonoBehaviour
{
    internal bool interactable;

    public Vector3 previousPos { get; private set; }

    private void LateUpdate()
    {
        previousPos = transform.position;
    }
}
