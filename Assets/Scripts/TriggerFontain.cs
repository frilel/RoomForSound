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

    public void InitiateSong2()
    {
        song2 = GameObject.Find("Song2GO(Clone)").GetComponent<Song2>();
        triggerState = song2.broadcastMarker;
    }

    private void Update() {

        if (GameObject.Find("Song2GO(Clone)") != null && !songActiveBool)
        {
            song2 = GameObject.Find("Song2GO(Clone)").GetComponent<Song2>();
            triggerState = song2.broadcastMarker;
            songActiveBool = true;
        }

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
