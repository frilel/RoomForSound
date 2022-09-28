using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCurrentLocation : MonoBehaviour
{

    public AudioManager audioManager;


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            audioManager.SetCurrentPosition(this.gameObject.name);
        }
   }
}
