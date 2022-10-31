using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class InstructionController : MonoBehaviour
{
    public int motionID = 0;
    public bool[] motionIDFinish = { false, false, false, false, false, false }; // holdcontroller, press button for sequencer, hold drumstick, teleport 
    bool enableInstruction = true;
    public GameObject canvasTips;
    public GameObject handL;
    public GameObject handR;
    [HideInInspector]
    public bool changeInstruction = false;
    public GameObject instructionCursor;
    bool success = true; // check whether user successfully do the right control 
    public bool wait = false; // wait to check whether user follows the instruction

    Vector3[] instructionPositions =
    {

        new Vector3(0.0f, 0f, 0.0f),
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(0.585f, 0.16f, -0.831f),
        new Vector3(0.8f, -0.1f, 0.0f),
        new Vector3(-0.5868f, -0.149f, -0.6812f),

    };
    Quaternion[] canvasTipsRotation =
    {

        Quaternion.Euler(0, 0, 0),
        Quaternion.Euler(0, 0, 0),
        Quaternion.Euler(0, 90, 0),
        Quaternion.Euler(0, 45, 0),
        Quaternion.Euler(0, -115, 0),
    };
    // Start is called before the first frame update
    void Start()
    {
        motionID = 0;
        StartCoroutine(MotionIDTimeCount(3f));
        transform.position = instructionPositions[motionID];
        enableInstruction = true;
        for (int i = 0; i < motionIDFinish.Length; i++) motionIDFinish[i] = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (enableInstruction)
        {
            if (motionIDFinish[motionID])
            {
                IncreaseMotionID();
            }
            if ((OVRInput.GetDown(OVRInput.RawButton.A)))
            {
                IncreaseMotionID();
            }
            if (motionID == instructionPositions.Length - 1)
            {
                handL.SetActive(true);
                handR.SetActive(false);
            }
        }

    }
    void IncreaseMotionID()
    {
        if (motionID < instructionPositions.Length - 1)
        {
            //Debug.Log("ss");
            motionIDFinish[motionID] = true;
            //if (motionIDFinish[motionID + 1] == true) motionID++; // skip the one that already finished 
            motionID++;
            transform.position = instructionPositions[motionID];
            canvasTips.transform.rotation = canvasTipsRotation[motionID];
        }
        else
        {
            instructionCursor.SetActive(false);
            transform.gameObject.SetActive(false);
            enableInstruction = false;
        }
    }
    void ChangeInstruction(bool continueOrNot)
    {

        if (continueOrNot && motionID < 2)
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
        ChangeInstruction(success);
    }
    public void handleAnimationEnd()
    {

        // determine to play next instruction or replay current ones 
        //wait = true;
        //if (motionID == 1) success = true;
        //StartCoroutine(MotionIDTimeCount(3f));
        if (motionID == instructionPositions.Length - 1)
        {
            instructionCursor.SetActive(false);
            transform.gameObject.SetActive(false);
            enableInstruction = false;
        }
    }
}
