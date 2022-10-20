using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStickSound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Stick")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Drumset/Sticks");
        }
    }

}
