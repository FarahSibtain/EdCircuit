using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MeasuringInstruments : Instrument
{
    TMP_Text measurement = null;
    // Start is called before the first frame update
    void Start()
    {
        measurement = gameObject.transform.Find("Measurement").GetComponent<TMP_Text>();
    }    

    public void SetText(string someText)
    {
        measurement.text = someText;
    }
}
