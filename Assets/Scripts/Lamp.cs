using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Instrument
{
    [SerializeField]
    Material turnONMaterial = null;

    [SerializeField]
    Material turnOFFMaterial = null;

    [SerializeField]
    GameObject[] GObjects = null;

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
