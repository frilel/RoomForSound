using UnityEngine;

public class TriggerFontain : MonoBehaviour
{
    ParticleSystem particles;
    Song2 song2;
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

    private void Update() {

        // check if one of the songs is deleted
        // as the song object will be destroyed, he passes this information to the button manager
        // songActiveBool is necessary only ONCE a song gameobject is detected
        // therefore the "gatekeeper" will be set back to false
        if(buttonManager.deleteMarker == "DELETE")
        {
            songActiveBool = false;
        }

        // this has to be done ONCE for each single song
        // as soon as we detect a song gameobject, we can get the script
        // further, we need once the state of the broadcast marker to compare it later if changes occured
        // we set the "gatekeeper" to true
        if (GameObject.Find("Song2GO(Clone)") != null && !songActiveBool)
        {
            song2 = GameObject.Find("Song2GO(Clone)").GetComponent<Song2>();
            triggerState = song2.broadcastMarker;
            songActiveBool = true;
        }

        // if the song is not null
        // check if a new marker is reached (changes in the triggerState)
        // start the particles and sound effect
        if(song2 != null) 
        {
            if(triggerState != song2.broadcastMarker)
            {
                triggerState = song2.broadcastMarker;
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
