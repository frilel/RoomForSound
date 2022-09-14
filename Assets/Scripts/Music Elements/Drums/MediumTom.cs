using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumTom : MonoBehaviour
{
  public void OnCollisionEnter() {
    FMODUnity.RuntimeManager.PlayOneShot("event:/MediumTom", transform.position);
  }
}
