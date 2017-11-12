using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour {

    public float grabBegin = 0.55f;
    public float grabEnd = 0.35f;
    public float speed = 2f;
    // 1
    private GameObject collidingObject;
    // 2
    private GameObject objectInHand;

    [SerializeField]
    protected OVRInput.Controller m_controller;

    protected float m_prevFlex;

    void FixedUpdate()
    {
        OnUpdatedAnchors();
    }

    void OnUpdatedAnchors()
    {
        float prevFlex = m_prevFlex;
        m_prevFlex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);
        CheckForGrabOrRelease(prevFlex);
    }

    protected void CheckForGrabOrRelease(float prevFlex)
    {
        if ((m_prevFlex >= grabBegin) && (prevFlex < grabBegin))
        {
            Debug.Log("grab");
            if (collidingObject)
            {
                GrabObject();
            }
        }
        else if ((m_prevFlex <= grabEnd) && (prevFlex > grabEnd))
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

            objectInHand.GetComponent<Rigidbody>().velocity = speed*OVRInput.GetLocalControllerVelocity(m_controller);
            objectInHand.GetComponent<Rigidbody>().angularVelocity = speed*OVRInput.GetLocalControllerAngularVelocity(m_controller);
        }
        // 4
        objectInHand = null;
    }

	
	// Update is called once per frame
	
}
