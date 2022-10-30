using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrumButtonTest : MonoBehaviour
{
    private GameObject sequenzer; 

    FMOD.Studio.EventInstance drumHitSFXInstance;
    public FMODUnity.EventReference eventPathInteractionSoundOne;

    private void Start() {
        sequenzer = GameObject.Find("Sequenzer");
    }
    public void PlayDrum()
    {
            drumHitSFXInstance = FMODUnity.RuntimeManager.CreateInstance(eventPathInteractionSoundOne);
            drumHitSFXInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(sequenzer));
            drumHitSFXInstance.start();
            drumHitSFXInstance.release();
    }

}
