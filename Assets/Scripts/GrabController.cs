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

    [SerializeField] bool inBasket = false;
    public GameObject firework;

    void Update()
    {
        OnUpdatedAnchors();

    }

    void OnUpdatedAnchors()
    {
        float prevFlex = m_prevFlex;
        m_prevFlex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);
        CheckForGrabOrRelease(prevFlex);

        if(m_prevFlex == 0)
        {
            ReleaseObject();
        }

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
                        f.GetComponent<Rigidbody>().useGravity = true;
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
            //finish grab tutorial
            if (GameObject.Find("tutorial_2"))
            { 
                Destroy(GameObject.Find("tutorial_2"), 3f);
            }
            //finish grab tutorial end
            //return;
        }

        if(other.tag == "SpecialFirework")
        {
            Debug.Log("colide with firework");
            SetCollidingObject(other);
        }

        if (other.tag == "firework")
        {
            Debug.Log("colide with firework");
            SetCollidingObject(other);
            //return;
        }
    }

    //public void OnTriggerStay(Collider other)
    //{
    //    SetCollidingObject(other);
    //}

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
        objectInHand = collidingObject;
        collidingObject = null;

        if (objectInHand.tag == "SpecialFirework")
        {
            objectInHand.transform.parent = null;
            objectInHand.GetComponent<Rigidbody>().useGravity = true;
        }

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        objectInHand.transform.parent = this.transform;
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

        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3
            objectInHand.transform.parent = null;
            objectInHand.GetComponent<Rigidbody>().velocity = speed*OVRInput.GetLocalControllerVelocity(m_controller);
            objectInHand.GetComponent<Rigidbody>().angularVelocity = speed*OVRInput.GetLocalControllerAngularVelocity(m_controller);
            
        }
        // 4
        objectInHand.transform.parent = null;
        objectInHand = null;
    }

	
	// Update is called once per frame
	
}
