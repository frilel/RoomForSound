using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public Material material;
    private List<MeshRenderer> ActiveMesh = new List<MeshRenderer>();

    public GameObject Song1GO;
    public GameObject Song2GO;
    public GameObject SongKallGO;
    public GameObject SongViniWeediWhiskeyGO;
    [HideInInspector]
    public GameObject currentSong = null;
    [HideInInspector]
    string currentPosition;
    [HideInInspector]
    public SongBase song2Script;
    [HideInInspector]
    public SongBase song1Script;
    [HideInInspector]
    public SongBase songKallScript;
    [HideInInspector]
    public SongBase songViniWeediWhiskeyScript;
    public MeshRenderer button03MeshRenderer;
    [HideInInspector]
    public Material red;
    [HideInInspector]
    public Material white;
    InstructionController instructionController;
    [HideInInspector]
    public string deleteMarker;
    public void Start()
    {
        instructionController = FindObjectOfType<InstructionController>();
    }

    public void SaveMaterialOfButton(MeshRenderer buttonMaterial)
    {
        ActiveMesh.Add(buttonMaterial);
    }

    public void ChangePreviousButtonMaterial()
    {
        if (!ActiveMesh.Count.Equals(0))
        {
            MeshRenderer mesh = ActiveMesh[0];
            mesh.material = material;
            ActiveMesh.Clear();
        }
    }

    public void ManageButtonPush(string buttonName, MeshRenderer buttonMeshRenderer)
    {
        if (instructionController != null)
        {
            instructionController.motionIDFinish[1] = true;
            instructionController.motionIDFinish[2] = true;
        }
        switch (buttonName)
        {
            // Play Song1
            case "Button01":
                PlaySong1();
                break;

            //  Play Song2
            case "Button02":
                PlaySong2(buttonMeshRenderer);
                break;
            case "ButtonKall":
                PlaySongKall();
                break;   
            case "ButtonViniWeediWhiskey":
                PlaySongViniWeediWhiskey(buttonMeshRenderer);
                break;                          

            // Turn current instrument Off 
            // (Once its off, it can't be turned on, there is a synchronisation issue wich causes the layers to unalign)
            case "Button03":
                if (song1Script != null)
                {
                    song1Script.TurnOffCurrentInstrument(currentPosition);
                }
                else if (song2Script != null)
                {
                    song2Script.TurnOffCurrentInstrument(currentPosition);
                }
                else if (songViniWeediWhiskeyScript != null)
                {
                    songViniWeediWhiskeyScript.TurnOffCurrentInstrument(currentPosition);
                }
                else if (songKallScript != null)
                {
                    songKallScript.TurnOffCurrentInstrument(currentPosition);
                }                                
                button03MeshRenderer = buttonMeshRenderer;
                button03MeshRenderer.material = red;
                break;

            // Stop the current song
            case "Button04":
                StopAndCheckStopSong();
                break;
            default:
                break;
        }
    }

    public void PlaySong1()
    {
        StopAndCheckStopSong();
        currentSong = Instantiate(Song1GO);
        song1Script = currentSong.GetComponent<SongBase>();
        GameManager.Instance.UpdateCurrentSongImage(GameManager.Song.Song1);
    }


    public void PlaySong2(MeshRenderer buttonMeshRenderer)
    {
        StopAndCheckStopSong();
        currentSong = Instantiate(Song2GO);
        song2Script = currentSong.GetComponent<SongBase>();
        if(buttonMeshRenderer != null)
            SaveMaterialOfButton(buttonMeshRenderer);
        GameManager.Instance.UpdateCurrentSongImage(GameManager.Song.Song2);
    }

    public void PlaySongKall()
    {
        StopAndCheckStopSong();
        currentSong = Instantiate(SongKallGO);
        songKallScript = currentSong.GetComponent<SongBase>();
        GameManager.Instance.UpdateCurrentSongImage(GameManager.Song.Kall);
    }

    public void PlaySongViniWeediWhiskey(MeshRenderer buttonMeshRenderer)
    {
        StopAndCheckStopSong();
        currentSong = Instantiate(SongViniWeediWhiskeyGO);
        songViniWeediWhiskeyScript = currentSong.GetComponent<SongBase>();
        if (buttonMeshRenderer != null)
            SaveMaterialOfButton(buttonMeshRenderer);
        GameManager.Instance.UpdateCurrentSongImage(GameManager.Song.ViniWeediWhiskey);
    }

    public void StopAndCheckStopSong()
    {
        ChangePreviousButtonMaterial();
        Destroy(currentSong);
        button03MeshRenderer.material = white;
        song2Script = null;
        song1Script = null;
        songViniWeediWhiskeyScript = null;
        songKallScript = null;

        GameManager.Instance.UpdateCurrentSongImage(GameManager.Song.NoSong);
    }

    public void SetCurrentLocationName(string currentPosition)
    {
        this.currentPosition = currentPosition;
    }
}
