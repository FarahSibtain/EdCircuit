using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class Move1 : MonoBehaviour

{
    private bool onTouchHold = false;
    private float depth = 0;

    //[SerializeField]
    private Camera arCamera = null;
    private Vector3 mOffset;

    private float mZCoord;

    Rigidbody rigidbd;

    private void Start()
    {
        Debug.Log("Im here");
        //arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
        arCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }
        Touch touch = Input.GetTouch(0);

        Vector2 touchPosition = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = arCamera.ScreenPointToRay(touch.position);
            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject))
            {
                if (hitObject.rigidbody.name == gameObject.name)
                {
                    onTouchHold = true;
                    depth = gameObject.transform.position.z;
                                        
                    rigidbd.constraints = RigidbodyConstraints.FreezePosition;
                }
            }
        }
        if (touch.phase == TouchPhase.Ended)
        {
            onTouchHold = false;
            rigidbd.constraints = RigidbodyConstraints.None;
        }
        if (onTouchHold)
        {
            Vector3 newPosition = new Vector3(touch.position.x, touch.position.y, depth - 0.2f);
            transform.SetPositionAndRotation(newPosition, Quaternion.identity);            
        }
    }
}
