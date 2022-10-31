using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public GameObject LeftHandAnchor;
    public GameObject RightHandAnchor;
    public GameObject CenterEyeAnchor;
    public GameObject Rig;
    public Image SongImageDisplay;
    public Transform audiencesParent;
    public GameObject[] musicians;
    public Transform[] grabbables;
    public GameObject handMenu;

    [Tooltip("Order this array with how the public enum Song is ordered. The song Enum 0 will correspond to position 0 in this array.")]
    [SerializeField] Sprite[] songImages;
    public GameObject Song2GO;

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
    private void Update()
    {
        if (handMenu.activeSelf)
        {
            Debug.Log("A");
            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                Debug.Log("B");
                Replay();
            }
        }
    }
    internal void UpdateCurrentSongImage(Song song)
    {
        SongImageDisplay.sprite = songImages[(int)song];
    }
    public void PlayerToDownStage()
    {
        foreach (GameObject go in musicians) //enable musicians
        {
            go.SetActive(true);
        }
        for (int i = 1; i < audiencesParent.childCount; i++)// make audiences cheer
        {
            audiencesParent.GetChild(i).GetComponent<AudienceControl>().ReturnAnimator().Play("Cheer");
        }
        foreach (Transform go in grabbables)// not show outline when downstage
        {
            go.GetComponent<Outline>().enabled = false;
        }
        ButtonManager buttonManager = FindObjectOfType<ButtonManager>();
        if (buttonManager.currentSong == null)
        {
            buttonManager.PlaySong2(null);
            UpdateCurrentSongImage(GameManager.Song.Song2);
        }
    }
    public void PlayerExitDownStage()
    {
        foreach (GameObject go in musicians)
        {
            go.SetActive(false);
        }
        foreach (Transform go in grabbables)
        {
            go.GetComponent<Outline>().enabled = true;
        }
    }
}
