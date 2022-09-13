using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFront : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/front", transform.position);
    }
}
