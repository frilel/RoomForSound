using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionController : MonoBehaviour
{
    public int motionID = 0;
    bool enableInstruction = true;
    bool success = true;
       
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MotionIDTimeCount(3f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeInstruction()
    {
        Debug.Log("change");
        if (success && motionID < 2)
        {
            motionID++;
            success = false;
        }
    }
    public IEnumerator MotionIDTimeCount(float delay)
    {
        Debug.Log("ie");
        yield return new WaitForSeconds(delay);
        ChangeInstruction();
    }
    public void handleAnimationEnd()
    {

    }
}
