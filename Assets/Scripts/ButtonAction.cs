using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    private GameObject cam;
    // private AudioManager audioManager;
    // private AudioMarkerManager audioMarkerManager;
    // public Text debugText;
    [SerializeField] public MeshRenderer ButtonMaterial;
    [SerializeField] public ButtonManager ButtonManager;
    public GameObject Song2GO;
    GameObject song2;

    private void Start() 
    {
            cam = GameObject.Find("OVRCameraRig"); 
            // audioManager = cam.GetComponent<AudioManager>();
    }

    public void TriggerAudio()
    {
        string buttonName = transform.parent.gameObject.name;
        // debugText.text = buttonName;

        switch (buttonName)
        {
            case "Button01":
                ChangePreviousButtonMaterial();
                // audioManager.PlaySong("Song1/Song1");
                PlaceInMaterialArray();
                break;
            case "Button02":
                ChangePreviousButtonMaterial();
                // audioManager.PlaySong("Song2/Song2");
                song2 = Instantiate(Song2GO);
                PlaceInMaterialArray();
                break;
            case "Button03":
                // audioManager.TurnOffCurrentInstrument();
                // ChangePreviousButtonMaterial();
                // audioManager.PlaySong("Song2/Song2");
                // PlaceInMaterialArray();
                break;
            case "Button04":
                ChangePreviousButtonMaterial();
                // audioManager.StopSong();
                Destroy(song2);
                break;
            default: 
                break;
        }
    }

    private void PlaceInMaterialArray()
    {
        ButtonManager.SaveMaterialOfButton(ButtonMaterial);
    }

    private void ChangePreviousButtonMaterial()
    {
        ButtonManager.ChangePreviousButtonMaterial();
    }
}