using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class TriggerFontain : MonoBehaviour
{
    ParticleSystem particles;
    public bool partOnOff = false;
    FMOD.Studio.EVENT_CALLBACK markerCallback;
    FMOD.Studio.EventInstance musicInstance;

    public FMODUnity.EventReference eventName;

     class TimelineInfo
    {
        // public int currentMusicBar = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    TimelineInfo timelineInfo;
    GCHandle timelineHandle;


    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        
        
        timelineInfo = new TimelineInfo();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        markerCallback = new FMOD.Studio.EVENT_CALLBACK(MarkerEventCallback);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(eventName);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo);
        // Pass the object through the userdata of the instance
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(markerCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        musicInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        musicInstance.start();
    }

    void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        timelineHandle.Free();
    }

    void OnGUI()
    {
        GUILayout.Box(String.Format("Last Marker = {0}", (string)timelineInfo.lastMarker));
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    FMOD.RESULT MarkerEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        // Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            if(type == FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER)
                {
                    var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                    timelineInfo.lastMarker = parameter.name;
                    StartParticles();
                }
            }
        return FMOD.RESULT.OK;
    }

    public void StartParticles()
    {
        particles.Play();
    }

    private void Update() {

        // Debug.Log((string)timelineInfo.lastMarker);

        // switch ((string)timelineInfo.lastMarker)
        // {
        //     case "e":
        //         particles.Play();
        //         break;
        //     case "Marker A":
        //         particles.Play();
        //         break;
        //     default:
        //         break;
        // }

    }

    
}
