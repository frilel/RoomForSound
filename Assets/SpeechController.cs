using System.Collections;
using System.Collections.Generic;
using TextSpeech;
using UnityEngine;

public class SpeechController : MonoBehaviour
{
    const string LANG_CODE = "en-US";

    void Start()
    {
        Setup(LANG_CODE);

        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback = OnSpeakStop;
    }

    #region Text to Speech

    public void StartSpeaking(string message)
    {
        TextToSpeech.Instance.StartSpeak(message);
    }

    public void StopSpeaking(string message)
    {
        TextToSpeech.Instance.StopSpeak();
    }

    void OnSpeakStart() 
    {
        Debug.Log("Talking startet...");
    }

    void OnSpeakStop() 
    {
        Debug.Log("Talking stopped...");
    }

    #endregion

    void Setup(string code) {
        TextToSpeech.Instance.Setting(code, 1, 1);
    }
}
