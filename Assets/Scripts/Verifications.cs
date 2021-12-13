using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Verifications : MonoBehaviour
{
    Button btnDisconnect = null;

    Button btnReset = null;

    List<Vector3> GOposition = new List<Vector3>();

    protected Text disconIndicator = null;

    protected Text errorText = null;

    // Start is called before the first frame update
    void Start()
    {
        btnDisconnect = GameObject.Find("btnDisconnect").GetComponent<Button>();

        btnDisconnect.onClick.AddListener(OnDisconnectBtnClick);

        btnReset = GameObject.Find("btnReset").GetComponent<Button>();

        btnReset.onClick.AddListener(OnResetBtnClick);

        errorText = GameObject.Find("ErrorText").GetComponent<Text>();

        disconIndicator = GameObject.Find("DisconnectionIndicator").GetComponent<Text>();

        SaveChildrenPositions();
    }

    private void SaveChildrenPositions()
    {
        int totalChildren = transform.childCount;
        for (int i = 0; i < totalChildren; i++)
        {
            GOposition.Add(transform.GetChild(i).localPosition);
        }
        // Get the wires child which is the last after the instruments
        Transform goTrans = transform.GetChild(totalChildren - 1);
        for (int i = 0; i < goTrans.childCount; i++)
        {
            GOposition.Add(goTrans.GetChild(i).localPosition);
        }
        //Get the obisolver child of the wire which is its first child
        goTrans = goTrans.GetChild(0);
        for (int i = 0; i < goTrans.childCount; i++)
        {
            GOposition.Add(goTrans.GetChild(i).localPosition);
        }
    }

    private void OnResetBtnClick()
    {
        ResetInstruments();
    }

    public void ResetInstruments()
    {
        DisconnectConnections();
        int totalChildren = transform.childCount;
        int x = 0;
        //Reset all children local position
        for (int i = 0; i < totalChildren; i++, x++)
        {
            transform.GetChild(i).localPosition = GOposition[x];
        }
        Transform goTrans = transform.GetChild(totalChildren - 1); //Get the wire gameobject
        for (int i = 0; i < goTrans.childCount; i++, x++)
        {
            goTrans.GetChild(i).localPosition = GOposition[x];
        }
        goTrans = goTrans.GetChild(0);  //Get the obisolver gameobject
        for (int i = 0; i < goTrans.childCount; i++, x++)
        {
            goTrans.GetChild(i).localPosition = GOposition[x];
        }
    }

    private void OnDisconnectBtnClick()
    {
        DisconnectConnections();
    }

    public virtual void DisconnectConnections()
    {
               
    }    
}
