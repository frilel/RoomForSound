using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    Joystick joystick;

    private void Start()
    {
        GUI.enabled = true;
    }

    void Update2()
    {
        joystick = Joystick.current;
        if (joystick == null)
        {
            if ((int)Time.realtimeSinceStartup % 2 == 0)
                Debug.Log("No joystick");
            return; // No gamepad connected.
        }

        if (joystick.stick.left.wasPressedThisFrame)
        {
            Debug.Log("pressed" + (int)Time.realtimeSinceStartup);
            DebugInVR.Instance.text.text = "joystick.stick.left.wasPressedThisFrame " + (int)Time.realtimeSinceStartup;
        }

        //Vector2 move = gamepad.leftStick.ReadValue();
        //DebugInVR.Instance.text.text = "leftstick: " + move.ToString();
    }

    private void OnGUI()
    {
        //GUI.Label(new Rect(200, 200, 300, 75), "Accel: " + Input.acceleration.ToString());
        //GUI.Label(new Rect(200, 300, 300, 75), gamepad?.leftStick.ReadValue().ToString());
    }
}