using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instrument : MonoBehaviour
{   
    public InstruConnector connection1;

    public InstruConnector connection2;        

    public bool IsConnected()
    {
        if (connection1.IsConnected() && connection2.IsConnected())
        {
            return true;
        }
        else
        {
            return false;
        }
    }    

    public List<String> GetConnectedInstrumentNames()
    {
        List<String> connectedInstruments = new List<string>();
        AddConnectedInstrumentNames(ref connectedInstruments, connection1.GetConnectedInstrumentNames());
        AddConnectedInstrumentNames(ref connectedInstruments, connection2.GetConnectedInstrumentNames());
        connectedInstruments.RemoveAll(ContainName);
        return connectedInstruments;
    }

    private bool ContainName(String s)
    {
        return s.Equals(this.gameObject.name);
    }

    private void AddConnectedInstrumentNames(ref List<string> connectedInstruments, List<string> instrumentNames)
    {
        foreach (string name in instrumentNames)
        {
            if (!connectedInstruments.Contains(name))
            {
                connectedInstruments.Add(name);
            }
        }
    }
    
    public void DisconnectConnections()
    {
        connection1.DisconnectConnections();
        connection2.DisconnectConnections();
    }
}
