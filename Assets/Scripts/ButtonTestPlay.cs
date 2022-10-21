using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTestPlay : MonoBehaviour
{
    public GameObject Song2GO;
    GameObject song2;

    public void PlayButtonSong()
    {
        song2 = Instantiate(Song2GO);
    }

    public void StopButtonSong()
    {
        Destroy(song2);
    }
}
