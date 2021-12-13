using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Instrument
{
    [SerializeField]
    Transform  keySwitch = null;

    bool isClosed = false;    

    public static bool IsDoubleTap()
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

    void open()
    {        
        keySwitch.localRotation = Quaternion.Euler(-50.0f, 90.0f, 0.0f);
        isClosed = false;
    }

    void close()
    {
       // keySwitch.Rotate(50.0f, 0.0f, 0.0f, Space.Self);
        keySwitch.localRotation = Quaternion.Euler(0f, 90.0f, 0.0f);
        isClosed = true;
    }

    public bool  IsClosed()
    {
        return isClosed;
    }

    private void Update()
    {
        if (IsDoubleTap())
        {
            if (isClosed)
            {
                open();
            }
            else
            {
                close();
            }
        }
    }
}
