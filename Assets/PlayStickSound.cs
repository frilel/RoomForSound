using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStickSound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Collision");
        if(other.gameObject.tag == "Stick")
        {
            Debug.Log("Stick");
            FMODUnity.RuntimeManager.PlayOneShot("event:/Drum set/Sticks");
        }
    }

}
