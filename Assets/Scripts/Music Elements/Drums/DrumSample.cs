using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSample : MonoBehaviour
{
  public FMODUnity.EventReference _eventPath;
  public int _windowSize = 512;
  public FMOD.DSP_FFT_WINDOW _windowShape = FMOD.DSP_FFT_WINDOW.RECT;
  public void OnCollisionEnter() {
    FMODUnity.RuntimeManager.PlayOneShot(_eventPath, transform.position);
  }
}
