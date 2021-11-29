using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstruConnector : MonoBehaviour
{
    bool isConnected = false;
    Instrument parentInstrument = null;
    public List<Wire> ConnectedWires = null;    

    public bool IsConnected()
    {
        return isConnected;
    }

    private void Start()
    {
        parentInstrument = gameObject.transform.parent.GetComponent<Instrument>();
        ConnectedWires = new List<Wire>();
    }

    private void ApplyConnection(Collision collision)
    {
        Vector3 pos = gameObject.transform.position;
        collision.gameObject.transform.position = new Vector3(pos.x, pos.y + 0.001f, pos.z);
        gameObject.AddComponent<FixedJoint>();
        gameObject.GetComponent<FixedJoint>().connectedBody = collision.rigidbody;

        //Set connected body information
        isConnected = true;
        Wire ConnectedWire = collision.gameObject.transform.parent.GetComponent<Wire>();
        ConnectedWires.Add(ConnectedWire);        
        collision.rigidbody.SendMessage("ApplyConnection", parentInstrument);
    }

    private void DisconnectConnections()
    {
        //Disconnect the connection
        FixedJoint[] fjComponents = gameObject.GetComponents<FixedJoint>();
        foreach (FixedJoint comp in fjComponents)
        {
            Rigidbody obj = comp.connectedBody;
            obj.SendMessage("DisconnectInstrument");
            Destroy(comp);
        }        

        //Reset connected game object information
        isConnected = false;
        ConnectedWires = new List<Wire>();        
    }    

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.tag == "Player")
        {
            ApplyConnection(collision);
        }
    }    

    private void Update()
    {
        if (isConnected && IsDoubleTap())
        {
            DisconnectConnections();
        }
    }

    bool IsDoubleTap()
    {
        bool result = false;
        float MaxTimeWait = 1;
        float VariancePosition = 1;

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            float DeltaTime = Input.GetTouch(0).deltaTime;
            float DeltaPositionLenght = Input.GetTouch(0).deltaPosition.magnitude;

            if (DeltaTime > 0 && DeltaTime < MaxTimeWait && DeltaPositionLenght < VariancePosition)
                result = true;
        }
        return result;
    }

    public List<string> GetConnectedInstrumentNames()
    {
        List<string> connectedInstruments = new List<string>();
        foreach (Wire wire in ConnectedWires)
        {
            List<string> connectedInstr = wire.GetConnectedInstrumentNames();
            foreach (string instrumentname in connectedInstr)
            {
                if (!connectedInstruments.Contains(instrumentname))
                {
                    connectedInstruments.Add(instrumentname);
                }
            }            
        }

        return connectedInstruments;
    }

}
