using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighTom : MonoBehaviour
{
  public void OnCollisionEnter() {
    FMODUnity.RuntimeManager.PlayOneShot("event:/HighTom", transform.position);
  }
}
