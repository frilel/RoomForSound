using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public Material material;
    private List<MeshRenderer> ActiveMesh = new List<MeshRenderer>();

    public GameObject Song1GO;
    public GameObject Song2GO;
    GameObject currentSong = null;
    string currentPosition;
    Song2 song2Script;
    Song1 song1Script;
    public MeshRenderer button03MeshRenderer;
    public Material red;
    public Material white;
    InstructionController instructionController;
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
        if(!ActiveMesh.Count.Equals(0))
        {
            MeshRenderer mesh = ActiveMesh[0];
            mesh.material = material;
            ActiveMesh.Clear();
        }
    }

        public void ManageButtonPush(string buttonName, MeshRenderer buttonMeshRenderer)
    {
        instructionController.motionIDFinish[1] = true;
        instructionController.motionIDFinish[2] = true;
        switch (buttonName)
        {
            // Play Song1
            case "Button01":
                StopAndCheckStopSong();
                currentSong = Instantiate(Song1GO);
                song1Script = currentSong.GetComponent<Song1>();
                SaveMaterialOfButton(buttonMeshRenderer);
                break;
            
            //  Play Song2
            case "Button02":
                StopAndCheckStopSong();
                currentSong = Instantiate(Song2GO);
                song2Script = currentSong.GetComponent<Song2>();
                SaveMaterialOfButton(buttonMeshRenderer);
                break;

            // Turn current instrument Off 
            // (Once its off, it can't be turned on, there is a synchronisation issue wich causes the layers to unalign)
            case "Button03":
                if(song1Script != null) 
                {
                    song1Script.TurnOffCurrentInstrument(currentPosition);
                }
                else if(song2Script != null) 
                {
                    song2Script.TurnOffCurrentInstrument(currentPosition);
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

    public void StopAndCheckStopSong() 
    {
        ChangePreviousButtonMaterial();
        Destroy(currentSong);
        button03MeshRenderer.material = white;
        song2Script = null;
        song1Script = null;
    }

    public void SetCurrentLocationName(string currentPosition)
    {
       this.currentPosition = currentPosition;
    }

}
