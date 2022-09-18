using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public FMODUnity.EventReference _eventPath;
    public void PlayAudio() {
        FMODUnity.RuntimeManager.PlayOneShot(_eventPath, transform.position);
    }
}
