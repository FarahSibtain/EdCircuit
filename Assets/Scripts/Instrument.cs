using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    [SerializeField]
    InstruConnector connection1;

    [SerializeField]
    InstruConnector connection2;

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
}
