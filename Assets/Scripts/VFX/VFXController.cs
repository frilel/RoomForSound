using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VFXController : MonoBehaviour
{

    public GameObject prefab;
    public void triggerOne(Transform position)
    {
        GameObject particle = Instantiate(prefab, position);

        //GameObject particle = Instantiate(prefab, position, Quaternion.identity);
        particle.GetComponentInChildren<ParticleSystem>().Play();

        OVRInput.SetControllerVibration(0.1f, 1, OVRInput.Controller.RTouch);
        this.CallWithDelay(stopVibration, 0.1f);
    }
    void stopVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}


