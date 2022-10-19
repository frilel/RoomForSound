using UnityEngine;

public class UImanager : MonoBehaviour
{

    public GameObject HandMenu;

    private void Start()
    {
        HandMenu.SetActive(false);
    }

    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            HandMenu.SetActive(true);
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.Y))
        {
            HandMenu.SetActive(false);
        }
    }

}
