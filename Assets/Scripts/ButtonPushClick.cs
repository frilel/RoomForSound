using System;
using System.Collections;
using UnityEngine;

public class ButtonPushClick : MonoBehaviour
{
    public float MinLocalY = -0.54f;
    private float MaxLocalY = 0.3675466f;

    public bool isClicked = false;

    public Material greenMat;

    public bool resetButton;
    Vector3 originalPos;

    void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, MaxLocalY, transform.localPosition.z);
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        // Save current position for LockButton
        Vector3 buttonDownPosition  = new Vector3(transform.localPosition.x, MinLocalY, transform.localPosition.z);
        // Save current position for Lerp
        Vector3 buttonUpPosition    = new Vector3(transform.localPosition.x, MaxLocalY, transform.localPosition.z);

        if (isClicked == false)
        {

            if(transform.localPosition.y > MaxLocalY)
            {
                transform.localPosition = originalPos;
            }

            if (transform.localPosition.y < MinLocalY)
            {
                LockButton(buttonDownPosition);
            }
        }

        if(resetButton) {
            resetButton = false;
            // StartCoroutine(UnlockButton());
            transform.localPosition = originalPos;
            isClicked = false;
            GetComponent<BoxCollider>().isTrigger = false;

        }
      
    }

    void LockButton(Vector3 lockPosition)
    {
        isClicked = true;               
        GetComponent<BoxCollider>().isTrigger = true;
        Vector3 NewDownPos  = new Vector3(transform.localPosition.x, MinLocalY, transform.localPosition.z);
        transform.localPosition = NewDownPos;
        transform.parent.transform.parent.GetComponent<ButtonManager>().UnlockPreviousButton();
        transform.parent.transform.parent.GetComponent<ButtonManager>().activeButton = this.gameObject;
        TriggerEvent();
    }

    private void TriggerEvent()
    {
        string buttonName = transform.parent.gameObject.name;

        GameObject camera = GameObject.Find("OVRCameraRig"); 

        AudioManager audioManager = camera.GetComponent<AudioManager>();

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
