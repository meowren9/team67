using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolControl : MonoBehaviour {
    public GameObject fishingReel;
    public GameObject directionA;
    public GameObject directionB;
    private Plane rotPlane;

    private bool isRotate = false;
    private float nextRot;
    private float epsilon = 0.001f;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ControlArea")
        { 
            isRotate = true;          
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "ControlArea")
        {
            isRotate = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        isRotate = false;
    }


    void Update()
    {
        if (!GameManager.debug)
        {
            if (!PhotonNetwork.isMasterClient) //p1
            {
                return;
            }
        }

        //Debug.Log("!!" + OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));
        //if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0f)
        ////if(OVRInput.Get(OVRInput.Button.One))
        if (1==1)
        {
            
            rotPlane = new Plane(fishingReel.transform.right, fishingReel.transform.position);
            DrawPlane(fishingReel.transform.right, fishingReel.transform.position);

            directionB.transform.position = rotPlane.ClosestPointOnPlane(transform.position);
            directionA.transform.LookAt(directionB.transform);

            if (directionA.transform.localEulerAngles.y > epsilon || directionA.transform.localEulerAngles.y < -epsilon)
            {
                nextRot = 0 - directionA.transform.localEulerAngles.x - 180f;
            }
            else
            {
                nextRot = directionA.transform.localEulerAngles.x;
            }

            if (isRotate)
            {
                fishingReel.transform.localEulerAngles = new Vector3(nextRot+80f, 0f, 0f);
                print("pp");
                OVRInput.SetControllerVibration(1, 20, OVRInput.Controller.RTouch);
            }

            if (!isRotate)
            {
                OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
            }
        }

        else
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }

    }

    private void DrawPlane(Vector3 normal, Vector3 position)
    {

        Vector3 v3;

        if (normal.normalized != Vector3.forward)
            v3 = Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude;
        else
            v3 = Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude; ;

        var corner0 = position + v3;
        var corner2 = position - v3;
        var q = Quaternion.AngleAxis(90.0f, normal);
        v3 = q * v3;
        var corner1 = position + v3;
        var corner3 = position - v3;

        Debug.DrawLine(corner0, corner2, Color.green);
        Debug.DrawLine(corner1, corner3, Color.green);
        Debug.DrawLine(corner0, corner1, Color.green);
        Debug.DrawLine(corner1, corner2, Color.green);
        Debug.DrawLine(corner2, corner3, Color.green);
        Debug.DrawLine(corner3, corner0, Color.green);
        Debug.DrawRay(position, normal, Color.red);
    }
}
