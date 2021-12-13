using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConectionVerfication : Verifications
{
    [SerializeField]
    Instrument Battery;

    [SerializeField]
    Lamp R1;

    [SerializeField]
    Lamp R2;

    [SerializeField]
    MeasuringInstruments A1;

    [SerializeField]
    MeasuringInstruments A2;

    [SerializeField]
    MeasuringInstruments V1;

    [SerializeField]
    MeasuringInstruments V2;

    [SerializeField]
    MeasuringInstruments V3;    

    bool updated = false;      

    // Update is called once per frame
    void Update()
    {
        if (MainCircuitVerifications())
        {
            R1.TurnOnLight();
            R2.TurnOnLight();

            A1.SetText("3 A");
            A2.SetText("3 A");

            errorText.text = "Success!!!";

            updated = true;

            if (VerifyVoltages(V1))
            {
                V1.SetText("6 V");
            }
            else
            {
                V1.SetText("");
            }

            if (VerifyVoltages(V2))
            {
                V2.SetText("6 V");
            }
            else
            {
                V2.SetText("");
            }

            if (VerifyV3Voltage())
            {
                V3.SetText("12 V");
            }
            else
            {
                V3.SetText("");
            }            
        }
        else
        {
            if (updated)
            {
                R1.TurnOffLight();
                R2.TurnOffLight();

                A1.SetText("");
                A2.SetText("");

                V1.SetText("");
                V2.SetText("");
                V3.SetText("");
            }
        }        
    }

    bool MainCircuitVerifications()
    {
        if (!MainCircuitConnected())
        {
            return false;
        }

        // Verification 2: Check if Battery is connected with Ammeters 
        List<string> connectedInstrumentsNames = Battery.GetConnectedInstrumentNames();
        if (!VerifyConnectedInstrumentNames(connectedInstrumentsNames, "A1", "A2"))
        {
            errorText.text = "Connect both Ammeters to Battery ";
            return false;
        }       

        //Verify that R1 and R2 are connected
        connectedInstrumentsNames = R1.GetConnectedInstrumentNames();
        if (!connectedInstrumentsNames.Contains("R2"))
        {
            errorText.text = "Connect R1 and R2";
            return false;
        }
        return true;
    }    

    bool VerifyVoltages(MeasuringInstruments Voltmeter)
    {
        if (!Voltmeter.IsConnected() || !Voltmeter.AreConnectedWiresClosed())
        {
            errorText.text = Voltmeter.gameObject.name + " or it's wire is not connected";
            return false;
        }

        //Verification 4: V1 & V2 are connected with either R1 or R2
        List<string> VconnectedInstrumentsNames = Voltmeter.GetConnectedInstrumentNames();
        if (!(VconnectedInstrumentsNames.Count == 1 && (VconnectedInstrumentsNames[0] == "R1" || VconnectedInstrumentsNames[0] == "R2")))
        {
            errorText.text = "Connect V1 and V2 to R1 or R2 singly. Check " + Voltmeter.gameObject.name;
            return false;
        }
       
        return true;
    }

    bool VerifyV3Voltage()
    {
        if (!V3.IsConnected())
        {
            errorText.text = "V3 is not connected";
            return false;
        }

        //Verification 3: V3 is connected with R1 & R2
        List<string> connectedInstrumentsNames = V3.GetConnectedInstrumentNames();
        if (!VerifyConnectedInstrumentNames(connectedInstrumentsNames, "R1", "R2"))
        {
            // errorText.text = "Connected Instruments: " + connectedInstrumentsNames.ToString();
            errorText.text = "Connect V3 to R1 and R2";
            return false;
        }        

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

    bool MainCircuitConnected()
    {
        if (!Battery.IsConnected() || !Battery.AreConnectedWiresClosed())
        {
            errorText.text = "Battery/Battery Wire is not connected";
            return false;
        }
        else if (!A1.IsConnected() || !A1.AreConnectedWiresClosed())
        {
            errorText.text = "A1/A1 Wire is not connected";
            return false;
        }
        else if (!A2.IsConnected() || !A2.AreConnectedWiresClosed())
        {
            errorText.text = "A2/A2 Wire is not connected";
            return false;
        }
        else if (!R1.IsConnected() || !R1.AreConnectedWiresClosed())
        {
            errorText.text = "R1/R1 Wire is not connected";
            return false;
        }
        else if (!R2.IsConnected() || !R2.AreConnectedWiresClosed())
        {
            errorText.text = "R2/R2 Wire is not connected";
            return false;
        }

        return true;
    }    

    public override void DisconnectConnections()
    {
        disconIndicator.text = "Disconnecting all connections";
        Battery.DisconnectConnections();
        R1.DisconnectConnections();
        R2.DisconnectConnections();
        A1.DisconnectConnections();
        A2.DisconnectConnections();
        V1.DisconnectConnections();
        V2.DisconnectConnections();
        V3.DisconnectConnections();
    }
}
