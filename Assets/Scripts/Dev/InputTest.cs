using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    Gamepad gamepad;

    private void Start()
    {
        GUI.enabled = true;
    }

    void Update()
    {
        DebugInVR.Instance.text.text = "Accel: " + Input.acceleration.ToString() + "\n";


        gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
            DebugInVR.Instance.text.text = "gamepad.rightTrigger.wasPressedThisFrame";
        }

        Vector2 move = gamepad.leftStick.ReadValue();
        DebugInVR.Instance.text.text = "leftstick: " + move.ToString();
    }

    private void OnGUI()
    {
        //GUI.Label(new Rect(200, 200, 300, 75), "Accel: " + Input.acceleration.ToString());
        //GUI.Label(new Rect(200, 300, 300, 75), gamepad?.leftStick.ReadValue().ToString());
    }
}