using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circuit3Verification : Verifications
{
    [SerializeField]
    Instrument Battery;

    [SerializeField]
    Lamp lamp;

    [SerializeField]
    MeasuringInstruments Ammeter;

    [SerializeField]
    MeasuringInstruments Voltmeter;

    [SerializeField]
    Key SwitchKey;

    // Update is called once per frame
    void Update()
    {        
        if (SwitchKey.IsClosed() && IsCircuitClosed())
        {
            lamp.TurnOnLight();
            Ammeter.SetText("3 A");
            if (IsVoltmeterConnected())
            {
                Voltmeter.SetText("12 A");
            }
        }
        else
        {
            lamp.TurnOffLight();
            Ammeter.SetText("");
            Voltmeter.SetText("");
            //errorText.text = "";
        }
    }

    private bool IsVoltmeterConnected()
    {
        // Battery
        Wire wire = Voltmeter.GetConnectionWire();

        if (wire == null)
        {
            errorText.text = "No wire connected to V3";
            return false;
        }

        List<string> instrNames = wire.GetConnectedInstrumentNames(Voltmeter.name);

        if (instrNames == null || instrNames.Count == 0 || instrNames[0] != "R1")
        {
            errorText.text = "V3 is supposed to connect with Lamp only";
            return false;
        }        

        wire = Voltmeter.GetOtherConnectedWire(wire);

        if (wire == null)
        {
            errorText.text = "V3 is not properly connected to lamp";
            return false;
        }

        instrNames = wire.GetConnectedInstrumentNames(Voltmeter.name);

        if (instrNames == null || instrNames.Count == 0 || instrNames[0] != "R1")
        {
            errorText.text = "V3 is not properly connected to lamp";
            return false;
        }
        else
        {
            errorText.text = "Circuit Complete!!!";
            return true;
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

            List<string> instrNames = wire.GetConnectedInstrumentNames(new List<string>() { Battery.name, Voltmeter.name });

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

            instrNames = wire.GetConnectedInstrumentNames(new List<string>() { instru.name, Voltmeter.name });

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

            instrNames = wire.GetConnectedInstrumentNames(new List<string>() { instru.name, Voltmeter.name });

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

            instrNames = wire.GetConnectedInstrumentNames(new List<string>() { instru.name, Voltmeter.name });

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
            Debug.Log("Exception Occurred: " + e.Message + e.InnerException);
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
        Voltmeter.DisconnectConnections();
        SwitchKey.DisconnectConnections();
    }
}
