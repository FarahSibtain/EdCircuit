using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    GameObject gobj = null;
    Plane objPlane;
    Vector3 m0;
    private bool onTouchHold = false;
    private bool isPlayer = false;

    public static GameObject SelectedObj = null;

    Text text;

    private void Start()
    {
        text = GameObject.FindObjectOfType<Text>();
    }

    private Ray GenerateMouseRay(Touch touch)
    {
        //Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 mousePosFar = new Vector3(touch.position.x, touch.position.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane);

        Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);

        Ray mr = new Ray(mousePosN, mousePosF - mousePosN);
        return mr;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        //if (Input.GetMouseButtonDown(0))
        if(touch.phase == TouchPhase.Began)
        {
            Ray mouseRay = GenerateMouseRay(touch);
            RaycastHit hit;

            if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit) && (hit.transform.gameObject.tag == "instrument" || hit.transform.gameObject.tag == "Player"))
            {
                SelectedObj = gobj = hit.transform.gameObject;
                objPlane = new Plane(Vector3.up, gobj.transform.position);

                //Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                Ray mRay = Camera.main.ScreenPointToRay(touch.position);
                float rayDistance;
                objPlane.Raycast(mRay, out rayDistance);
                m0 = gobj.transform.position - mRay.GetPoint(rayDistance);
                onTouchHold = true;
                //if (gobj.tag == "Player")
                //{
                //    Rigidbody rb = gobj.GetComponent<Rigidbody>();
                //    rb.constraints = RigidbodyConstraints.FreezePosition;

                //    text.text = "Constraints applied on " + gobj.name;
                //    isPlayer = true;
                //}
            }
        }
        // else if (Input.GetMouseButtonUp(0) && gobj)
        if (touch.phase == TouchPhase.Ended && gobj)
        {
            Ray mRay = Camera.main.ScreenPointToRay(touch.position);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
            {
                //gobj.transform.SetPositionAndRotation(mRay.GetPoint(rayDistance) + m0, Quaternion.identity);
                gobj.transform.position = mRay.GetPoint(rayDistance) + m0;
            }
            onTouchHold = false;
            //if (gobj.tag == "Player")
            //{
            //    Rigidbody rb = gobj.GetComponent<Rigidbody>();
            //    rb.constraints = RigidbodyConstraints.None;

            //    text.text = "Constraints removed from " + gobj.name;
            //    isPlayer = false;
            //}
            SelectedObj = gobj = null;
        }
        //else if (Input.GetMouseButton(0) && gobj)
        if (onTouchHold && gobj)
        {
            Ray mRay = Camera.main.ScreenPointToRay(touch.position);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
            {
                //if (isPlayer)
                //{
                //    
                //}
                rayDistance -= 0.05f;
                //gobj.transform.SetPositionAndRotation(mRay.GetPoint(rayDistance) + m0, Quaternion.identity);
                gobj.transform.position = mRay.GetPoint(rayDistance) + m0;
            }
        }       
    }
}
