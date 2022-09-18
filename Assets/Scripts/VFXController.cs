using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VFXController : MonoBehaviour
{

    public GameObject prefab;
    public void triggerOne()
    {
        GameObject particle = Instantiate(prefab);
        particle.GetComponentInChildren<ParticleSystem>().Play();
    }

}


