using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTom : MonoBehaviour
{
  public void OnTriggerEnter() {
    FMODUnity.RuntimeManager.PlayOneShot("event:/FloorTom", transform.position);
  }
}
