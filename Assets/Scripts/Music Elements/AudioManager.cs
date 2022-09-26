using UnityEngine;

public class AudioManager : MonoBehaviour
{
    FMOD.Studio.EventInstance song;

    public void PlaySong(string songName)
    {
        song = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/Song1/" + songName);
        song.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        song.start();
        song.release();

    }

    public void StopSong()
    {
        song.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void AudienceCheer()
    {

    }

    public void AudienceWhistle()
    {

    }
}
