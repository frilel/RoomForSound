using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.Assertions;

public class SongBase : MonoBehaviour
{
    FMOD.Studio.EVENT_CALLBACK beatCallback;
    FMOD.Studio.EventInstance musicInstance;

    public FMODUnity.EventReference eventName;

    // C# EVENTS
    public delegate void SongStart(SongBase song);
    public static event SongStart OnSongStart;

    class TimelineInfo
    {
        public int currentMusicBar = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    Transform cam;
    ButtonManager buttonManager;
    [HideInInspector]
    public string broadcastMarker;


    // Start is called before the first frame update
    void Start()
    {
        // cam = GameObject.Find("OVRCameraRig").transform;
        buttonManager = GameObject.Find("Sequenzer").GetComponent<ButtonManager>();
        cam = GameManager.Instance?.Rig.transform.Find("OVRCameraRig");
        Assert.IsNotNull(cam, "Please add the prefab GameManager to your scene!");

        timelineInfo = new TimelineInfo();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(eventName);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo);
        // Pass the object through the userdata of the instance
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        musicInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(cam));
        musicInstance.start();

        OnSongStart(this);
    }

    void OnDestroy()
    {
        buttonManager.deleteMarker = "DELETE";
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
        timelineHandle.Free();
    }

    public void TurnOffCurrentInstrument(string currentLocation)
    {
        float value;
        musicInstance.getParameterByName(currentLocation, out value);
        if(value == 1) 
        {
            musicInstance.setParameterByName(currentLocation, 0f);
        }
    }

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
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
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentMusicBar = parameter.bar;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;
    }

    private void Update() {
        broadcastMarker = timelineInfo.lastMarker;
    }

}