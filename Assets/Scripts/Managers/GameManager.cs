using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public GameObject LeftHandAnchor;
    public GameObject RightHandAnchor;
    public GameObject CenterEyeAnchor;
    public GameObject Rig;
    public Image SongImageDisplay;

    [Tooltip("Order this array with how the public enum Song is ordered. The song Enum 0 will correspond to position 0 in this array.")]
    [SerializeField] Sprite[] songImages;

    public enum Song
    {
        Song1,
        Song2,
        Kall,
        ViniWeediWhiskey
    }

    private void Awake()
    {
        Instance = this;
    }
    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

    internal void UpdateCurrentSong(Song song)
    {
        SongImageDisplay.sprite = songImages[(int)song];
    }
}
