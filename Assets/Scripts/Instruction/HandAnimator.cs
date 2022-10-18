using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimator : MonoBehaviour
{
    public InstructionController instructionController;
    // Start is called before the first frame update
    void Start()
    {
        instructionController = GetComponentInParent<InstructionController>();
    }
    public void onAnimationEnd()
    {
        instructionController.handleAnimationEnd();
    }

}
