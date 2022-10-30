using FMODUnity;
using UnityEngine;

public class TriggerFontain : MonoBehaviour
{
    ParticleSystem particles;
    Song2 song2;
    string triggerState;
    bool songActiveBool = false;

    FMOD.Studio.EventInstance flameThrowShort;
    


    private void Start() {
        particles = GetComponent<ParticleSystem>();
        flameThrowShort = FMODUnity.RuntimeManager.CreateInstance("event:/FlameThrow/FlameThrowShort");
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

        // Debug.Log(song2);
        if(song2 != null) 
        {
            if(triggerState != song2.broadcastMarker)
            {
                flameThrowShort.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                flameThrowShort.start();
                triggerState = song2.broadcastMarker;
                StartParticles();

            }
        }

    }

    public void StartParticles()
    {
        particles.Play();
    } 
}
