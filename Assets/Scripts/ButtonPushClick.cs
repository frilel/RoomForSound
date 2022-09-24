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
    Vector3 originalPos;

    void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, MaxLocalY, transform.localPosition.z);
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        // Save current position for LockButton
        Vector3 buttonDownPosition  = new Vector3(transform.localPosition.x, MinLocalY, transform.localPosition.z);
        // Save current position for Lerp
        Vector3 buttonUpPosition    = new Vector3(transform.localPosition.x, MaxLocalY, transform.localPosition.z);



        if (isClicked == false)
        {
            if (transform.localPosition.y < MaxLocalY)
            {
                MoveButtonSlowlyBackToOrigin(buttonUpPosition);

            }

            if(transform.localPosition.y > MaxLocalY)
            {
                // GetComponent<BoxCollider>().isTrigger = true;
                transform.localPosition = originalPos;
                // GetComponent<BoxCollider>().isTrigger = false;
            }

            if (transform.localPosition.y < MinLocalY)
            {
                LockButton(buttonDownPosition);
            }
        }

        if(resetButton) {
            resetButton = false;
            // StartCoroutine(UnlockButton());
            transform.localPosition = originalPos;
            isClicked = false;
            GetComponent<BoxCollider>().isTrigger = false;

        }
      
    }

    void MoveButtonSlowlyBackToOrigin(Vector3 updateToOrigin) 
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, updateToOrigin, Time.deltaTime * smooth);
    }

    void LockButton(Vector3 lockPosition)
    {
        isClicked = true;               
        GetComponent<BoxCollider>().isTrigger = true;
        Vector3 NewDownPos  = new Vector3(transform.localPosition.x, MinLocalY, transform.localPosition.z);
        transform.localPosition = NewDownPos;
        transform.parent.transform.parent.GetComponent<ButtonManager>().UnlockPreviousButton();
        transform.parent.transform.parent.GetComponent<ButtonManager>().activeButton = this.gameObject;
    }

    // IEnumerator UnlockButton() 
    // {
    //     isClicked = false;
    //     Vector3 buttonOrigin = new Vector3(transform.localPosition.x, MaxLocalY, transform.localPosition.z);
    //     float timeElapsed = 0;
    //     float lerpDuration = 1; 
    //     while(timeElapsed < lerpDuration)
    //     {         
    //         transform.localPosition = Vector3.Lerp(transform.localPosition, buttonOrigin, Time.deltaTime * 2f);   
    //         yield return null;
    //         timeElapsed += Time.deltaTime;
    //     }
    //     // GetComponent<BoxCollider>().enabled = true;
    //     GetComponent<BoxCollider>().isTrigger = false;
    // }
}
