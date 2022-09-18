using UnityEngine;

public class DrumStick : MonoBehaviour
{
    internal bool interactable = true;

    public Vector3 PreviousPos { get; private set; }
    public float TimeSincePrevPosSave { get; private set; } = 0f;

    private float previousPosResetTime = 100f; //100ms

    private void Update()
    {
        TimeSincePrevPosSave += Time.deltaTime;
        if (TimeSincePrevPosSave > previousPosResetTime)
        {
            PreviousPos = transform.position;
            TimeSincePrevPosSave = 0f;
        }

    }
}
