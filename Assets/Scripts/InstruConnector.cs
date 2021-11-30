using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstruConnector : MonoBehaviour
{
    bool isConnected = false;
    Instrument parentInstrument = null;
    List<Wire> ConnectedWires = null;
    AudioSource connectionSound = null;
    Text errorText = null;

    public bool IsConnected()
    {
        return isConnected;
    }

    private void Start()
    {
        parentInstrument = gameObject.transform.parent.GetComponent<Instrument>();
        ConnectedWires = new List<Wire>();
        errorText = GameObject.Find("DisconnectionIndicator").GetComponent<Text>();
        connectionSound = GameObject.Find("ConnectionSound").GetComponent<AudioSource>();
    }

    private void ApplyConnection(Collision collision)
    {
        Vector3 pos = gameObject.transform.position;
        collision.gameObject.transform.position = collision.GetContact(0).point;// new Vector3(pos.x, pos.y + 0.001f, pos.z);        
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = collision.rigidbody;
        joint.enableCollision = false;
        collision.rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        connectionSound.Play();

        //Set connected body information
        isConnected = true;
        WireConnector wireConnector = collision.gameObject.GetComponent<WireConnector>();
        Wire ConnectedWire = wireConnector.PartOfWire;
        ConnectedWires.Add(ConnectedWire);
        wireConnector.ApplyConnection(parentInstrument);
        errorText.text = "";
    }

    private void DisconnectConnections()
    {
        errorText.text = "Douple Tap received! Disconnecting all connections";
        //Disconnect the connection
        FixedJoint[] fjComponents = gameObject.GetComponents<FixedJoint>();
        foreach (FixedJoint comp in fjComponents)
        {
            Rigidbody obj = comp.connectedBody;    
            if (obj != null)
            {
                obj.SendMessage("DisconnectInstrument");
                obj.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                errorText.text = "No rigidbody was found in the joint";
            }
            
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
        float MaxTimeWait = 0.5f;
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

            //if any wire is not currently connected to this instrument Connector, then remove the wire from the array
            if (!connectedInstr.Contains(parentInstrument.gameObject.name))
            {                
                ConnectedWires.Remove(wire);
            }
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
