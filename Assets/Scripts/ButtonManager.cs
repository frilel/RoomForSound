using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Text ListShow;
    public Material material;
    public List<MeshRenderer> ActiveMesh = new List<MeshRenderer>();

    public void SaveMaterialOfButton(MeshRenderer ButtonMaterial)
    {
        ActiveMesh.Add(ButtonMaterial);
    }

    private void Update() {
        /*for (int i = 0; i < ActiveMesh.Count; i++)
        {
           ListShow.text = ActiveMesh[i].material.ToString();
        }*/
    }

    public void ChangePreviousButtonMaterial()
    {
        if(!ActiveMesh.Count.Equals(0))
        {
            MeshRenderer mesh = ActiveMesh[0];
            mesh.material = material;
            ActiveMesh.Clear();
        }
    }
}
