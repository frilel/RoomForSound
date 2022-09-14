using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassDrum : MonoBehaviour
{
  public void OnCollisionEnter() {
    FMODUnity.RuntimeManager.PlayOneShot("event:/BassDrum", transform.position);
  }
}
