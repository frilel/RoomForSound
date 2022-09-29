using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        if (gamepad.rightTrigger.wasPressedThisFrame)
        {
            DebugInVR.Instance.text.text = "gamepad.rightTrigger.wasPressedThisFrame";
        }

        Vector2 move = gamepad.leftStick.ReadValue();
        DebugInVR.Instance.text.text = move.ToString();
    }
}