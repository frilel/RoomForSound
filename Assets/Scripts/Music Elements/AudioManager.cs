using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private FMOD.Studio.EventInstance song;
    private string currentPosition;

    public Text debugText;

    public void PlaySong(string songName)
    {
        song.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        song = FMODUnity.RuntimeManager.CreateInstance("event:/Songs/" + songName);
        song.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        song.start();
        song.release();

    }

    public void SetCurrentPosition(string currentPosition)
    {
       this.currentPosition = currentPosition;
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

    public void TurnOffCurrentInstrument()
    {
        debugText.text = currentPosition;
        float value;
        song.getParameterByName(currentPosition, out value);

        if(value == 0) 
        {
            song.setParameterByName(currentPosition, 1f);
        }
        if(value == 1) 
        {
            song.setParameterByName(currentPosition, 0f);
        }

    }
}
