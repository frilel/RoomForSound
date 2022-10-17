using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StandNoteControl : MonoBehaviour
{
    public Text chatText;
    string chatContent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateChat(string name, string newText)
    {
        chatContent="【"+name+"】"+": "+newText+"\n"+chatContent;
        chatText.text=chatContent;
    }
}
