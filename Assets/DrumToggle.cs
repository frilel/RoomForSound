using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumToggle : MonoBehaviour
{
    public GameObject drumStandard;
    public GameObject drumVariation; 

    // Start is called before the first frame update
    void Start()
    {
        //drumStandard = GameObject.Find("DrumStandard");
        //drumVariation = GameObject.Find("DrumVariation");
        //Debug.Log("sss");
        drumVariation.SetActive(false);
    }

    public void toggleDrumSet()
    {
        if(drumStandard.activeSelf)
        {
            drumStandard.SetActive(false);
            drumVariation.SetActive(true);
        }
        else
        {
            drumStandard.SetActive(true);
            drumVariation.SetActive(false);
        }
    }
}
