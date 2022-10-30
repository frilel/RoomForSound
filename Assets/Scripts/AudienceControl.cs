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
    private Color nameColor;
    private bool isDisplayingMessage = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetChild(1).GetComponent<Animator>();
        nameColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);
        nameText.color = nameColor;
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
        if (audience.messages.Count > 0 && !isDisplayingMessage)
        {
            isDisplayingMessage = true;
            UpdateMessage();
        }
    }
    public void UpdateDisplay()
    {
        nameText.text = audience.name;
    }
    public void UpdateMessage()
    {
        animator = transform.GetChild(avatarIndex).GetChild(1).GetComponent<Animator>();
        if (audience.messages[0].message == messageText.text)
        {
            return;
        }
        string text = audience.messages[0].message;
        //Debug.Log("got message from " + audience.name + ": " + text);
        foreach (StandNoteControl snc in FindObjectsOfType<StandNoteControl>())
        {
            snc.UpdateChat(audience.name, audience.messages[0].message, nameColor);
        }
        StartCoroutine("DeleteMessage");
        messagePanel.SetActive(false);
        messageText.text = text;
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
            case "Firework":
            case "firework":
                animator.Play("Firework");
                break;
            case "Heart":
            case "heart":
                animator.Play("Heart");
                break;

            default:
                messagePanel.SetActive(true);
                messageText.text = text;
                break;
        }


    }
    IEnumerator DeleteMessage()
    {
        //yield return new WaitForSeconds(1.5f);
        string url = "https://roomforsound-server.herokuapp.com/messages?id=" + audience.messages[0].id;
        UnityWebRequest www = UnityWebRequest.Delete(url);
        //Debug.Log("Message Deleted");
        yield return new WaitForSeconds(1.5f);
        messageText.text = "no message here, type something";
        messagePanel.SetActive(false);
        isDisplayingMessage = false;
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
