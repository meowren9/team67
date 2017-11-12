using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour {


    // 1
    private GameObject collidingObject;
    // 2
    private GameObject objectInHand;

    [SerializeField]
    protected OVRInput.Controller m_controller;

    void Update()
    {

        //Debug.Log("update");
       // //if (Controller.GetHairTriggerDown())
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("grab");
            if (collidingObject)
            {
                GrabObject();
            }
        }

        // 2
        //if (Controller.GetHairTriggerUp())
        if (Input.GetKeyUp(KeyCode.M))
        {
            Debug.Log("release");
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }
    private void SetCollidingObject(Collider col)
    {
        // 1
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // 2
        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {

        Debug.Log("colide");
        SetCollidingObject(other);
    }

    // 2
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject()
    {
        // 1
        objectInHand = collidingObject;
        collidingObject = null;
        // 2
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // 3
    private FixedJoint AddFixedJoint()
    {
        Debug.Log("add fix");
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3

            //OVRPose localPose = new OVRPose { position = OVRInput.GetLocalControllerPosition(m_controller), orientation = OVRInput.GetLocalControllerRotation(m_controller) };
            //OVRPose offsetPose = new OVRPose { position = m_anchorOffsetPosition, orientation = m_anchorOffsetRotation };
            //localPose = localPose * offsetPose;

            //OVRPose trackingSpace = transform.ToOVRPose() * localPose.Inverse();
            //Vector3 linearVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerVelocity(m_controller);
            //Vector3 angularVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerAngularVelocity(m_controller);

            //objectInHand.GetComponent<Rigidbody>().velocity = linearVelocity;
            //objectInHand.GetComponent<Rigidbody>().angularVelocity = angularVelocity;

            objectInHand.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(m_controller);
            objectInHand.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(m_controller);
        }
        // 4
        objectInHand = null;
    }

	
	// Update is called once per frame
	
}
