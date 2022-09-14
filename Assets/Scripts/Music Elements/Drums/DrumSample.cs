using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSample : MonoBehaviour
{
  public FMODUnity.EventReference _eventPath;
  public void OnCollisionEnter() {
    FMODUnity.RuntimeManager.PlayOneShot(_eventPath, transform.position);
  }
}
