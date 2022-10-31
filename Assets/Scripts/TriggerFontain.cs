using FMODUnity;
using UnityEngine;

public class TriggerFontain : MonoBehaviour
{
    ParticleSystem particles;
    SongBase song1;
    SongBase song2;
    SongBase songKall;
    SongBase songViniWeediWhiskey;

    string triggerState;
    ButtonManager buttonManager;
    public bool songActiveBool = false;

    FMOD.Studio.EventInstance flameThrowShort;
    


    private void Start() {
        buttonManager = GameObject.Find("Sequenzer").GetComponent<ButtonManager>();

        particles = GetComponent<ParticleSystem>();
        flameThrowShort = FMODUnity.RuntimeManager.CreateInstance("event:/FlameThrow/FlameThrowShort");
        flameThrowShort.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    private void OnEnable()
    {
        SongBase.OnSongStart += OnSongStart;
    }

    private void OnDestroy()
    {
        SongBase.OnSongStart -= OnSongStart;
    }

    private void OnSongStart(SongBase songFromEvent)
    {

        Debug.Log(songFromEvent.eventName.ToString());
        DebugInVR.Instance.text.text = songFromEvent.eventName.ToString();
        triggerState = songFromEvent.broadcastMarker;

        switch (songFromEvent.eventName.ToString())
        {
            #if UNITY_EDITOR
                case "{925f5ce9-c73e-4584-a6d7-faa26dc74321} (event:/Songs/Song1/Song1)":
                    song1 = songFromEvent;
                break;
                case "{cb4d4287-038f-4ac1-acb3-f2fb468add2a} (event:/Songs/Song2/Song2)":
                    song2 = songFromEvent;
                break;
                case "{94864161-aaf3-4f08-92dc-02b5e5725d6d} (event:/Songs/Vini Weedi Whiskey/Vini Weedi Whiskey)":
                    songViniWeediWhiskey = songFromEvent;
                break;            
                case "{12dccf4b-16cf-43b9-af09-c9705430bb70} (event:/Songs/Kall ft. BenG/Kall ft. BenG)":
                    songKall = songFromEvent;
                break;            
            #else
                case "{925f5ce9-c73e-4584-a6d7-faa26dc74321}":
                    song1 = songFromEvent;
                break;
                case "{cb4d4287-038f-4ac1-acb3-f2fb468add2a}":
                    song2 = songFromEvent;
                break;
                case "{94864161-aaf3-4f08-92dc-02b5e5725d6d}":
                    songViniWeediWhiskey = songFromEvent;
                break;            
                case "{12dccf4b-16cf-43b9-af09-c9705430bb70}":
                    songKall = songFromEvent;
                break;
            #endif
        }
    }

    private void Update() {

        // if the song is not null
        // check if a new marker is reached (changes in the triggerState)
        // start the particles and sound effect
        if(song1 != null) 
        {
            if(triggerState != song1.broadcastMarker)
            {
                triggerState = song1.broadcastMarker;
                StartParticles();
            }
        }
        // Debug.Log(song2);
        if(song2 != null) 
        {
            //Debug.Log(song2.broadcastMarker);
            if(triggerState != song2.broadcastMarker)
            {
                triggerState = song2.broadcastMarker;
                StartParticles();
            }
        }
        if(songKall != null) 
        {
            if(triggerState != songKall.broadcastMarker)
            {
                triggerState = songKall.broadcastMarker;
                StartParticles();
            }
        }
        if(songViniWeediWhiskey != null) 
        {
            if(triggerState != songViniWeediWhiskey.broadcastMarker)
            {
                triggerState = songViniWeediWhiskey.broadcastMarker;
                StartParticles();
            }
        }        
    }

    public void StartParticles()
    {
        flameThrowShort.start();
        particles.Play();
    } 
}
