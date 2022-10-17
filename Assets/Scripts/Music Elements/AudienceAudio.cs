using UnityEngine;

public class AudienceAudio : MonoBehaviour
{
    FMOD.Studio.EventInstance clap;

    private void Start() {
      
    }
    public void StartClap()
    {
        clap = FMODUnity.RuntimeManager.CreateInstance("event:/Audience/Clapping");
        clap.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        clap.start();
        clap.release();
    }

    public void StartCheering()
    {
        clap = FMODUnity.RuntimeManager.CreateInstance("event:/Audience/Cheering");
        clap.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        clap.start();
        clap.release();
    }
}
