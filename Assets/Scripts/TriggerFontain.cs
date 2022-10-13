using UnityEngine;

public class TriggerFontain : MonoBehaviour
{
    ParticleSystem particles;
    Song2 song2;
    string triggerState;
    bool songActiveBool = false;

    private void Start() {
        particles = GetComponent<ParticleSystem>();
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
            Debug.Log(song2.broadcastMarker);
            Debug.Log(triggerState);
            if(triggerState != song2.broadcastMarker)
            {
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
