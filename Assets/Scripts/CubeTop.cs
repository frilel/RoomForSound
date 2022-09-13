using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/top", transform.position);
    }
}
