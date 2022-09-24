using System.Collections;
using UnityEngine;

public class ButtonPushClick : MonoBehaviour
{
    public float MinLocalY;
    public float MaxLocalY;

    public bool isClicked = false;

    public Material greenMat;

    public bool resetButton;

    public float smooth = 0.1f;

    void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, MaxLocalY, transform.localPosition.z);
    }

    private void Update()
    {
        // Save current position for LockButton
        Vector3 buttonDownPosition  = new Vector3(transform.localPosition.x, MinLocalY, transform.localPosition.z);
        // Save current position for Lerp
        Vector3 buttonUpPosition    = new Vector3(transform.localPosition.x, MaxLocalY, transform.localPosition.z);

        if (isClicked == false)
        {
            if (transform.localPosition.y > MaxLocalY  || transform.localPosition.y < MaxLocalY)
            {
                MoveButtonSlowlyBackToOrigin(buttonUpPosition);

            }

            if (transform.localPosition.y < MinLocalY)
            {
                LockButton(buttonDownPosition);
            }
        }

        if(resetButton) {
            resetButton = false;
            StartCoroutine(UnlockButton());
        }
      
    }

    void MoveButtonSlowlyBackToOrigin(Vector3 updateToOrigin) 
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, updateToOrigin, Time.deltaTime * smooth);
    }

    void LockButton(Vector3 lockPosition)
    {
        isClicked = true;               
        GetComponent<BoxCollider>().enabled = false;
        transform.parent.transform.parent.GetComponent<ButtonManager>().UnlockPreviousButton();
        transform.parent.transform.parent.GetComponent<ButtonManager>().activeButton = this.gameObject;
    }

    IEnumerator UnlockButton() 
    {
        isClicked = false;
        Vector3 buttonOrigin = new Vector3(transform.localPosition.x, MaxLocalY, transform.localPosition.z);
        while(transform.localPosition != buttonOrigin)
        {         
            transform.localPosition = Vector3.Lerp(transform.localPosition, buttonOrigin, Time.deltaTime * 2f);   
            yield return null;
        }
        GetComponent<BoxCollider>().enabled = true;
    }
}
