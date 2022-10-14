using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInstruction : MonoBehaviour
{
    InstructionController instructionController;
    public Animator rightHandAnimator;
    // public Animator leftHandAnimator;
    public Animator rightOculusHandAnimator;
    public Animator leftOculusHandAnimator;
    private int prevMotionID = -1;

    void Start()
    {
        if (instructionController == null)
            instructionController = GetComponentInParent<InstructionController>();
    }
    void Update()
    {
        rightHandAnimator.SetInteger("motionID", instructionController.motionID);
        rightOculusHandAnimator.SetInteger("motionID", instructionController.motionID);
        rightHandAnimator.SetBool("wait", instructionController.wait);
        rightOculusHandAnimator.SetBool("wait", instructionController.wait);
        /*        if (instructionController.changeInstruction)
                {
                    rightHandAnimator.SetInteger("motionID", instructionController.motionID);
                    rightOculusHandAnimator.SetInteger("motionID", instructionController.motionID);
                }*/


    }


}
