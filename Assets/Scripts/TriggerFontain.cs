using FMODUnity;
using UnityEngine;

public class TriggerFontain : MonoBehaviour
{
    ParticleSystem particles;
    Song1 song1;
    Song2 song2;
    SongKall songKall;
    SongViniWeediWhiskey songViniWeediWhiskey;

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
        Song2.OnSong2Start += OnSong2Start;
    }

    private void OnDestroy()
    {
        Song2.OnSong2Start -= OnSong2Start;
    }

    private void OnSong2Start(Song2 song2fromEvent)
    {
        triggerState = song2fromEvent.broadcastMarker;
        song2 = song2fromEvent;
    }

    public void InitiateSong2()
    {
        song2 = GameObject.Find("Song2GO(Clone)").GetComponent<Song2>();
        triggerState = song2.broadcastMarker;
    }

    private void Update() {

        // check if one of the songs is deleted
        // as the song object will be destroyed, he passes this information to the button manager
        // songActiveBool is necessary only ONCE a song gameobject is detected
        // therefore the "gatekeeper" will be set back to false
        if(buttonManager.deleteMarker == "DELETE")
        {
            songActiveBool = false;
        }


        if (GameObject.Find("Song1GO(Clone)") != null && !songActiveBool)
        {
            song1 = GameObject.Find("Song1GO(Clone)").GetComponent<Song1>();
            triggerState = song1.broadcastMarker;
            songActiveBool = true;
        }
        if (GameObject.Find("SongKallGO(Clone)") != null && !songActiveBool)
        {
            songKall = GameObject.Find("SongKallGO(Clone)").GetComponent<SongKall>();
            triggerState = songKall.broadcastMarker;
            songActiveBool = true;
        }
        if (GameObject.Find("SongViniWeediWhiskeyGO(Clone)") != null && !songActiveBool)
        {
            songViniWeediWhiskey = GameObject.Find("SongViniWeediWhiskeyGO(Clone)").GetComponent<SongViniWeediWhiskey>();
            triggerState = songViniWeediWhiskey.broadcastMarker;
            songActiveBool = true;
        }


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
