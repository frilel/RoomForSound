using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StandNoteControl : MonoBehaviour
{
    public Text chatText;
    //public Tex
    string chatContent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateChat(string name, string newText,Color nameColor)
    {
        string nameToAdd=$"【<color=#{ColorUtility.ToHtmlStringRGBA( nameColor )}>{name}</color>】";
        string messageToAdd="";
        switch(newText)
        {
            case "Dance":
            case "dance":
                messageToAdd=" is dancing!";
                break;
            case "Clap":
            case "clap":
                messageToAdd=" is clapping!";
                break;
            case "Idle":
            case "idle":
                messageToAdd=" is causally standing now.";
                break;
            case "Wave":
            case "wave":
                messageToAdd=" is waving!";
                break;
            case "Cheer":
            case "cheer":
                messageToAdd=" is cheering!";
                break;
            case "Firework":
            case "firework":
                messageToAdd=" sent a firework!";
                break;
            case "Heart":
            case "heart":
                messageToAdd=" sent a heart!";
                break;
            default:
                messageToAdd=": "+newText;
                break;
        }
        
        chatContent=nameToAdd+messageToAdd+"\n"+chatContent;
        chatText.text=chatContent;
    }
}
