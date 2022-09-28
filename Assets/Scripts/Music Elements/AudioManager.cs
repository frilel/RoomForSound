using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance song;

    public void PlaySong(string songName)
    {
        song.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        song = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/" + songName);
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
