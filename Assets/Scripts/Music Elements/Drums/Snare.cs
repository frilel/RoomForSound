using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snare : MonoBehaviour
{
  public void OnCollisionEnter() {
    FMODUnity.RuntimeManager.PlayOneShot("event:/Snare", transform.position);
  }
}
