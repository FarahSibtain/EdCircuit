using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Controller : MonoBehaviour
{
    ObiRopeCursor cursor;
    ObiRope rope;

    // Use this for initialization
    void Start()
    {
        //cursor = GetComponentInChildren<ObiRopeCursor>();
        cursor = GetComponent<ObiRopeCursor>();
        rope = cursor.GetComponent<ObiRope>();
        cursor.ChangeLength(rope.restLength - 10f * Time.deltaTime);
    }    
}
