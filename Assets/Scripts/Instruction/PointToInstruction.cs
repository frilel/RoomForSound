using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToInstruction : MonoBehaviour
{
    InstructionController instructionController;
    
    // Start is called before the first frame update
    void Start()
    {
        instructionController = FindObjectOfType<InstructionController>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetToPos = instructionController.transform.position;
        Vector3 diff = targetToPos - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, diff, Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        //float angle = AngleBetweenVector2(transform.position, targetToPos);
        //transform.eulerAngles = new Vector3(0, 0, Vector3.Angle(transform.position, targetToPos));
        //float a = Vector3.Angle(diff, transform.forward);
        //float angle = Vector3.SignedAngle(transform.position, targetToPos, Vector3.up); //Returns the angle between -180 and 180.
        //if (angle < 0)
        //{
        //    angle = 360 - angle * -1;
        //}
        //transform.eulerAngles = new Vector3(0, 0, -angle);
    }
    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        float angle = 0;
        //We get the angle between two vectors
        angle = Vector3.Angle(vec1, vec2);
        //We get the sign (1 or -1) depending the position of the vectors using the foward direction
        float sign = Mathf.Sign(Vector3.Dot(Vector3.forward, Vector3.Cross(vec1, vec2)));
        float signed_angle = angle * sign;
        //We return the angle
        return signed_angle;
    }
}
