using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circuit1Verification : Verifications
{
    [SerializeField]
    Instrument Battery;

    [SerializeField]
    Lamp lamp;

    [SerializeField]
    MeasuringInstruments Ammeter;

    [SerializeField]
    Key SwitchKey;   

    // Update is called once per frame
    void Update()
    {
        if (SwitchKey.IsClosed() && IsCircuitClosed())
        {
            lamp.TurnOnLight();
            Ammeter.SetText("3 A");
        }
        else
        {
            lamp.TurnOffLight();
            Ammeter.SetText("");
            errorText.text = "";
        }
    }

    bool IsCircuitClosed()
    {
        try
        {
            // Battery
            Wire wire = Battery.GetConnectionWire();

            if (wire == null)
            {
                errorText.text = "No wire connected to Battery";
                return false;
            }

            List<string> instrNames = wire.GetConnectedInstrumentNames(Battery.name);

            if (instrNames == null || instrNames.Count == 0)
            {
                errorText.text = "No instrument connected to 1st wire";
                return false;
            }

            // Instrument 1
            Instrument instru = GetConnectedInstrumentFromName(instrNames[0]);

            wire = instru.GetOtherConnectedWire(wire);

            if (wire == null)
            {
                errorText.text = "No wire connected to first instrument";
                return false;
            }

            instrNames = wire.GetConnectedInstrumentNames(instru.name);

            if (instrNames == null || instrNames.Count == 0)
            {
                errorText.text = "No instrument connected to 2nd wire";
                return false;
            }

            // Instrument 2
            instru = GetConnectedInstrumentFromName(instrNames[0]);

            wire = instru.GetOtherConnectedWire(wire);

            if (wire == null)
            {
                errorText.text = "No wire connected to second instrument";
                return false;
            }

            instrNames = wire.GetConnectedInstrumentNames(instru.name);

            if (instrNames == null || instrNames.Count == 0)
            {
                errorText.text = "No instrument connected to 3rd wire";
                return false;
            }

            // Instrument 3
            instru = GetConnectedInstrumentFromName(instrNames[0]);

            wire = instru.GetOtherConnectedWire(wire);

            if (wire == null)
            {
                errorText.text = "No wire connected to third instrument";
                return false;
            }

            instrNames = wire.GetConnectedInstrumentNames(instru.name);

            if (instrNames == null || instrNames.Count == 0)
            {
                errorText.text = "No instrument connected to 4th wire";
                return false;
            }

            if (instrNames[0] == Battery.name)
            {
                errorText.text = "Circuit closed!";
                return true;    //Circuit is completed so return true
            }

            errorText.text = "Battery not connected to 4th wire";
            return false;
        }
        catch(System.Exception e)
        {
            errorText.text = "Exception!!!";
            Debug.Log("Exception Occurred: " + e.Message);
            return false;
        }
    }

    Instrument GetConnectedInstrumentFromName(string instrName)
    {
        switch (instrName)
        {
            case "R1":
                return lamp;

            case "A1":
                return Ammeter;

            case "key":
                return SwitchKey;
        }

        return null;
    }

    public override void DisconnectConnections()
    {
        disconIndicator.text = "Disconnecting all connections";
        Battery.DisconnectConnections();
        lamp.DisconnectConnections();        
        Ammeter.DisconnectConnections();
        SwitchKey.DisconnectConnections();        
    }    
}
