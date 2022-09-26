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
    public Animator animator;
    private int avatarIndex;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
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
        if (audience.messages[0].message == messageText.text)
        {
            return;
        }
        string text = audience.messages[0].message;
        Debug.Log("got message from " + audience.name + ": " + text);
        foreach (StandNoteControl snc in FindObjectsOfType<StandNoteControl>())
        {
            snc.UpdateChat(audience.name, audience.messages[0].message);
        }
        StartCoroutine("DeleteMessage");
        switch (text)
        {
            case "Dance":
            case "dance":
                animator.Play("Dance");
                break;
            case "Clap":
            case "clap":
                animator.Play("Clap");
                break;
            case "Idle":
            case "idle":
                if (avatarIndex == 0)
                {
                    animator.Play("FemaleIdle");
                }
                else
                {
                    animator.Play("MaleIdle");
                }
                break;
            case "Wave":
            case "wave":
                animator.Play("Wave");
                break;
            case "Cheer":
            case "cheer":
                animator.Play("Cheer");
                break;

            default:
                break;
        }
        messagePanel.SetActive(true);
        messageText.text = text;

    }
    IEnumerator DeleteMessage()
    {
        yield return new WaitForSeconds(2);
        messagePanel.SetActive(false);
        string url = "https://roomforsound-server.herokuapp.com/messages?id=" + audience.messages[0].id;
        UnityWebRequest www = UnityWebRequest.Delete(url);
        Debug.Log("Message Deleted");
        yield return www.SendWebRequest();
    }
    public void ChangeAvatar(int index)
    {
        avatarIndex = index;
        for (int i = 0; i < 4; i++)
        {
            if (i == index)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                animator = transform.GetChild(i).GetChild(1).GetComponent<Animator>();

            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

        }
        if (avatarIndex == 0)
        {
            animator.Play("FemaleIdle");
        }
        else
        {
            animator.Play("MaleIdle");
        }
    }

}
