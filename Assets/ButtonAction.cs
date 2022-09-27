using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    private GameObject cam;
    private AudioManager audioManager;
    public Text debugText;

    private void Start() 
    {
            cam = GameObject.Find("OVRCameraRig"); 
            audioManager = cam.GetComponent<AudioManager>();
    }

    public void TriggerAudio()
    {
        string buttonName = transform.parent.gameObject.name;
        debugText.text = buttonName;

        switch (buttonName)
        {
            case "Button01":
                audioManager.PlaySong("Song1/Song1");
                break;
            case "Button02":
                audioManager.PlaySong("Song2/Song2");
                break;
            case "Button04":
                audioManager.StopSong();
                break;
            default: 
                break;
        }
    }
}