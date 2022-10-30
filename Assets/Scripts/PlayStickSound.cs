using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStickSound : MonoBehaviour
{
    FMOD.Studio.EventInstance stickColiideInstance;
    FMOD.Studio.PLAYBACK_STATE state;
    public bool isMainStick=false;
    private void Start()
    {
        stickColiideInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Drumset/Sticks");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stick"&&isMainStick)
        {
            stickColiideInstance.getPlaybackState(out state);
            if (state != FMOD.Studio.PLAYBACK_STATE.PLAYING)
            {
                Debug.Log("ssssssssss");
                stickColiideInstance.start();
                
            }
            //FMODUnity.RuntimeManager.PlayOneShot("event:/Drumset/Sticks");
        }
    }

}
