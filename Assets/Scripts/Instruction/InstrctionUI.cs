using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InstrctionUI : MonoBehaviour
{
    public TMPro.TMP_Text tip;
    InstructionController instructionController;
    string[] instructionText =
{
        "Welcome! Some useful tips would appear here",
        "First, adjust your hand position to hold controller",
        "Use middle finger to press [Grab button] and poke with your index finger to select one music",
        "Use middle finger to press [Grab button] to grab the drumstick",
        "Try teleport, press [X] on your left controller and point towards the teleport points and release!"
    };

    // Start is called before the first frame update
    void Start()
    {
        instructionController = GetComponentInParent<InstructionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (instructionController.motionID >= 0)
            tip.SetText(instructionText[instructionController.motionID]);
    }
}
