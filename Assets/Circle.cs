using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(Time.time)*8;
        float y = transform.position.y;
        float z = Mathf.Cos(Time.time)*8;
        transform.position = new Vector3(x,y,z);
    }
}
