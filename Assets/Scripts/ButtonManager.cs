using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // public List<string> buttons = new List<string>();
    public GameObject activeButton = null;


    internal void UnlockPreviousButton()
    {
        if(activeButton != null)
        {
            activeButton.GetComponent<ButtonPushClick>().resetButton = true;
        }
    }
}
