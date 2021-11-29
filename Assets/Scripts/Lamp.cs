using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField]
    Material turnONMaterial = null;

    [SerializeField]
    Material turnOFFMaterial = null;

    [SerializeField]
    GameObject[] GObjects;

    public void TurnOnLight()
    {
        SetMaterial(turnONMaterial);
    }

    public void TurnOffLight()
    {
        SetMaterial(turnOFFMaterial);
    }

    private void SetMaterial(Material material)
    {
        foreach(GameObject obj in GObjects)
        {
            obj.GetComponent<Renderer>().material = material;
        }
    }    
}
