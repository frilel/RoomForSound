using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    private GameObject cam;
    private AudioManager audioManager;
    public Text debugText;
    [SerializeField] public MeshRenderer ButtonMaterial;
    [SerializeField] public ButtonManager ButtonManager;

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
                ChangePreviousButtonMaterial();
                audioManager.PlaySong("Song1/Song1");
                PlaceInMaterialArray();
                break;
            case "Button02":
                ChangePreviousButtonMaterial();
                audioManager.PlaySong("Song2/Song2");
                PlaceInMaterialArray();
                break;
            case "Button04":
                ChangePreviousButtonMaterial();
                audioManager.StopSong();
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