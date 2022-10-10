using UnityEngine;

public class TriggerFontain : MonoBehaviour
{
    ParticleSystem particles;
    AudioMarkerManager audioMarkerManager;
    string triggerState;

    private void Start() {
        particles = GetComponent<ParticleSystem>();
        audioMarkerManager = GameObject.Find("OVRCameraRig").GetComponent<AudioMarkerManager>();
        triggerState = audioMarkerManager.broadcastMarker;
    }

    private void Update() {
        if(triggerState != audioMarkerManager.broadcastMarker)
        {
            triggerState = audioMarkerManager.broadcastMarker;
            StartParticles();
        }
    }

    public void StartParticles()
    {
        particles.Play();
    } 
}
