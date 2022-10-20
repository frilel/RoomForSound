using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clapping : MonoBehaviour
{
    FMOD.Studio.EventInstance clap;

    private void Start() {
      
    }
    public void StartClap()
    {
        clap = FMODUnity.RuntimeManager.CreateInstance("event:/Audience/Clapping");
        clap.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        clap.start();
        clap.release();
    }
}