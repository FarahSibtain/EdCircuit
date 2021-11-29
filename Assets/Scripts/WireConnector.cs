using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireConnector : MonoBehaviour
{
    bool isConnected;
    string WireName = "";
    public Instrument ConnectedInstrument = null;

    [SerializeField]
    public Wire PartOfWire = null;

    public bool IsConnected()
    {
        return isConnected;
    }

    private void Start()
    {
        WireName = PartOfWire.gameObject.name;
    }    

    public void ApplyConnection(Instrument connectedInstrument)
    {
        isConnected = true;
        ConnectedInstrument = connectedInstrument;
    }

    public void DisconnectInstrument()
    {
        isConnected = false;
        ConnectedInstrument = null;
    }

    public string GetConnectedInstrumentName()
    {
        return ConnectedInstrument.gameObject.name;
    }
}
