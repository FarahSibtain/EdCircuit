using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConectionVerfication : MonoBehaviour
{
    [SerializeField]
    Instrument[] Instruments;

    Lamp lamp1;
    Lamp lamp2;

    private void Start()
    {
        lamp1 = Instruments[1].GetComponent<Lamp>();
        lamp2 = Instruments[2].GetComponent<Lamp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DoAllVerifications())
        {
            lamp1.TurnOnLight();
            lamp2.TurnOnLight();
        }
        else
        {
            lamp1.TurnOffLight();
            lamp2.TurnOffLight();
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
        // instrConnectStatuses[0] -> Battery
        List<string> connectedInstrumentsNames = Instruments[0].GetConnectedInstrumentNames();
        if (!VerifyConnectedInstrumentNames(connectedInstrumentsNames, "A1", "A2"))
        {
            return false;
        }

        //Verification 3: V3 is connected with R1 & R2
        List<string> V3connectedInstrumentsNames = Instruments[7].GetConnectedInstrumentNames();
        if (!VerifyConnectedInstrumentNames(connectedInstrumentsNames, "R1", "R2"))
        {
            return false;
        }

        //Verification 4: V1 & V2 are connected with either R1 or R2
        List<string> VconnectedInstrumentsNames = Instruments[6].GetConnectedInstrumentNames();
        if (!(VconnectedInstrumentsNames.Count == 1 && (VconnectedInstrumentsNames[0] == "R1" || VconnectedInstrumentsNames[0] == "R2")))
        {
            return false;
        }

        string remainingVoltageName = VconnectedInstrumentsNames[0] == "R1" ? "R2" : "R1";
        VconnectedInstrumentsNames = Instruments[5].GetConnectedInstrumentNames();
        if (!(VconnectedInstrumentsNames.Count == 1 && VconnectedInstrumentsNames[0] == remainingVoltageName))
        {
            return false;
        }

        //After all verifications turn on the Lamps by returning true
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
        foreach (var instrument in Instruments)
        {
            if (!instrument.IsConnected())
            {
                return false;
            }
        }

        return true;
    }
}
