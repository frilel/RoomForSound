using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 

public class AudioManagerFM : MonoBehaviour
{
    public void PlayAudio() 
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/First Action", transform.position);
    }
}
