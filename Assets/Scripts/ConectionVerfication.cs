using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConectionVerfication : MonoBehaviour
{
    [SerializeField]
    Instrument Battery;

    [SerializeField]
    Lamp R1;

    [SerializeField]
    Lamp R2;

    [SerializeField]
    Instrument A1;

    [SerializeField]
    Instrument A2;

    [SerializeField]
    Instrument V1;

    [SerializeField]
    Instrument V2;

    [SerializeField]
    Instrument V3;    

    bool updated = false;

    Text errorText = null;

    private void Start()
    {
        errorText = GameObject.Find("ErrorText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DoAllVerifications())
        {
            R1.TurnOnLight();
            R2.TurnOnLight();

            A1.SetText("3 A");
            A2.SetText("3 A");

            V1.SetText("6 V");
            V2.SetText("6 V");
            V3.SetText("12 V");

            updated = true;
        }
        else
        {
            if (updated)
            {
                R1.TurnOffLight();
                R2.TurnOffLight();

                A1.SetText();
                A2.SetText();

                V1.SetText();
                V2.SetText();
                V3.SetText();
            }
        }        
    }

    bool DoAllVerifications()
    {
        //Verification 1: Check if all instruments are connected
        if (!AreAllInstrConnected())
        {
            return false;
        }

        // Verification 2: Check if Battery is connected with Ammeters 
       /* List<string> connectedInstrumentsNames = Battery.GetConnectedInstrumentNames();
        if (!VerifyConnectedInstrumentNames(connectedInstrumentsNames, "A1", "A2"))
        {
            errorText.text = "Connected both Ammeters to Battery";
            return false;
        }

        //Verification 3: V3 is connected with R1 & R2
        List<string> V3connectedInstrumentsNames = V3.GetConnectedInstrumentNames();
        if (!VerifyConnectedInstrumentNames(connectedInstrumentsNames, "R1", "R2"))
        {
            errorText.text = "Connect V3 to R1 and R2";
            return false;
        }

        //Verification 4: V1 & V2 are connected with either R1 or R2
        List<string> VconnectedInstrumentsNames = V2.GetConnectedInstrumentNames();
        if (!(VconnectedInstrumentsNames.Count == 1 && (VconnectedInstrumentsNames[0] == "R1" || VconnectedInstrumentsNames[0] == "R2")))
        {
            errorText.text = "Connect V1 and V2 to measure voltage of either R1 or R2 singly. Check V2";
            return false;
        }

        string remainingVoltageName = VconnectedInstrumentsNames[0] == "R1" ? "R2" : "R1";
        VconnectedInstrumentsNames = V1.GetConnectedInstrumentNames();
        if (!(VconnectedInstrumentsNames.Count == 1 && VconnectedInstrumentsNames[0] == remainingVoltageName))
        {
            errorText.text = "Connect V1 and V2 to measure voltage of either R1 or R2 singly. Check V1";
            return false;
        }*/

        //After all verifications turn on the Lamps by returning true
        errorText.text = "Success!!!";
        return true;
    }

    private bool VerifyConnectedInstrumentNames(List<string> connectedInstrumentsNames, string intru1, string instru2)
    {
        // Return false if battery is connected with exactly 2 instruments
        if (connectedInstrumentsNames.Count != 2)
        {
            return false;
        }
        //If battery is connected with the Ammeters then only return true
        if (connectedInstrumentsNames.Contains(intru1) && connectedInstrumentsNames.Contains(instru2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    

    private bool AreAllInstrConnected()
    {
        if (!Battery.IsConnected())
        {
            errorText.text = "Battery is not connected";
            return false;
        }
        else if (!A1.IsConnected())
        {
            errorText.text = "A1 is not connected";
            return false;
        }
        else if (!A2.IsConnected())
        {
            errorText.text = "A2 is not connected";
            return false;
        }
        else if (!R1.IsConnected())
        {
            errorText.text = "R1 is not connected";
            return false;
        }
        else if (!R2.IsConnected())
        {
            errorText.text = "R2 is not connected";
            return false;
        }
        else if (!V1.IsConnected())
        {
            errorText.text = "V1 is not connected";
            return false;
        }
        else if (!V2.IsConnected())
        {
            errorText.text = "V2 is not connected";
            return false;
        }
        else if (!V3.IsConnected())
        {
            errorText.text = "V3 is not connected";
            return false;
        }

        return true;
    }
}
