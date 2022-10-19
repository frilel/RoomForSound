using TMPro;
using UnityEngine;

public class DebugInVR : MonoBehaviour
{
    public static DebugInVR Instance;
    public TMP_Text text;

    private void Start()
    {
        Instance = this;
    }
}
