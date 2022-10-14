using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VFXController : MonoBehaviour
{

    public GameObject[] prefabs;
    public void triggerOne(Transform position)
    {

        GameObject particle = Instantiate(prefabs[Random.Range(0, 3)], position);

        //GameObject particle = Instantiate(prefab, position, Quaternion.identity);
        particle.GetComponentInChildren<ParticleSystem>().Play();

    }
    public void triggerVibration(OVRInput.Controller controller, float delay, float freq, float amplitude)
    {
        OVRInput.SetControllerVibration(freq, amplitude, controller);
        this.CallWithDelay(stopVibration, delay);

    }
    public void stopVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);

    }
}


