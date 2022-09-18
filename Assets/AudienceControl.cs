using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class AudienceControl : MonoBehaviour
{
    public Text nameText;
    public Text messageText;
    public GameObject messagePanel;
    public Audience audience;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (audience != null)
        {
            if (audience.messages.Count > 0)
            {
                UpdateMessage();
            }
        }*/
    }
    public void UpdateAuience(Audience a)
    {
        audience = a;
        UpdateDisplay();
        if (audience.messages.Count > 0)
        {
            UpdateMessage();
        }
    }
    public void UpdateDisplay()
    {
        nameText.text = audience.name;
    }
    public void UpdateMessage()
    {
        Debug.Log("got message from "+audience.name+": "+audience.messages[0].message);
        messagePanel.SetActive(true);
        messageText.text = audience.messages[0].message;
        StartCoroutine("DeleteMessage");
    }
    IEnumerator DeleteMessage()
    {
        yield return new WaitForSeconds(2);
        messagePanel.SetActive(false);
        string url="https://roomforsound-server.herokuapp.com/messages?id="+audience.messages[0].id;
        UnityWebRequest www = UnityWebRequest.Delete(url);
        Debug.Log("Message Deleted");
        yield return www.SendWebRequest();
    }
    public void ChangeAvatar(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == index)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }

}
