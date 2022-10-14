using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionController : MonoBehaviour
{
    public int motionID = 0;
    bool enableInstruction = true;
    [HideInInspector]
    public bool changeInstruction = false;
    bool success = true;
    public bool wait = false;
    Vector3[] instructionPositions =
    {

        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.2f, -0.5f),
    };
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MotionIDTimeCount(3f));
        transform.position = instructionPositions[motionID];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeInstruction()
    {
        
        if (success && motionID < 2)
        {
            // changeInstruction = true;
            motionID++;
            wait = false;
            transform.position = instructionPositions[motionID];

            success = false;
            //changeInstruction = false;
        }
    }
    public IEnumerator MotionIDTimeCount(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        ChangeInstruction();
    }
    public void handleAnimationEnd()
    {

        // determine to play next instruction or replay current ones 
        wait = true;
        if (motionID == 1) success = true;
        StartCoroutine(MotionIDTimeCount(3f));

    }
}
