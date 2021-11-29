using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField]
    WireConnector connector1;

    [SerializeField]
    WireConnector connector2;    

    public bool isConnected()
    {
        if (connector1.IsConnected() && connector2.IsConnected())
        {
            return true;
        }
        return false;
    }

    public List<string> GetConnectedInstrumentNames()
    {
        List<string> connectedInstruments = new List<string>();
        connectedInstruments.Add(connector1.GetConnectedInstrumentName());
        if (!connectedInstruments.Contains(connector2.GetConnectedInstrumentName()))
        {
            connectedInstruments.Add(connector2.GetConnectedInstrumentName());
        }
        return connectedInstruments;
    }

    //public string OtherConnectedInstrumentName(string instrument1)
    //{
    //    string instru1 = connector1.ConnectedInstrumentName;

    //    string instru2 = connector2.ConnectedInstrumentName;

    //    if (string.Compare(instru1, instrument1) == 0)
    //    {
    //        return instru2;
    //    }
    //    else if (string.Compare(instru2, instrument1) == 0)
    //    {
    //        return instru1;
    //    }//if instrument1 name does not match with the connected instruments of this wire then it is not connected to this wire, so return empty
    //    return "";
    //}
}
