﻿using System.Collections;
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

    [SerializeField] bool inBasket = false;
    public GameObject firework;

    void Update()
    {
        OnUpdatedAnchors();
        //debug
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (inBasket)
        //    {
        //        GameObject f = Instantiate(firework, this.transform.position,this.transform.rotation);
        //        collidingObject = f;
        //        GrabObject();
        //    }
        //}

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    ReleaseObject();
        //}

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
            
            if (collidingObject)
            {
                //Debug.Log("grab1");
                GrabObject();
            }
            else
            {
                if(inBasket)
                {
                    //Debug.Log("grab2");
                    if(GameManager.debug)
                    {
                        GameObject f = Instantiate(firework, this.transform.position, this.transform.rotation);
                        collidingObject = f;
                    }
                    else
                    {
                        GameObject f = PhotonNetwork.Instantiate(this.firework.name, this.transform.position, this.transform.rotation, 0);
                        f.GetComponent<Rigidbody>().useGravity = true;
                        collidingObject = f;
                    }
                    GrabObject();
                }
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
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "basket")
        {
            inBasket = true;
        }

        if(other.tag == "firework")
        {
            Debug.Log("colide with firework");
            SetCollidingObject(other);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "basket")
        {
            inBasket = false;
        }

        if (!collidingObject)
        {
            return;
        }
        collidingObject = null;
    }

    private void GrabObject()
    {
        //todo:
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
