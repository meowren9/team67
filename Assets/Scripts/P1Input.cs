using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Input : MonoBehaviour {


    bool EnterArea;
    bool pressTrigger;

    void Update()
    {
        if(!GameManager.debug)
        {
            if (!PhotonNetwork.isMasterClient) //p1
            {
                return;
            }
        }


        if (Database.moveUp)
        {
            this.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
        }

        if (Database.moveDown)
        {
            this.transform.position += new Vector3(0.0f, -0.1f, 0.0f);
        }

        if (EnterArea)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Database.handDrag = true;
        }

        if (!EnterArea)
        {
            Database.handDrag = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Database.handDrag = false;
            EnterArea = false;
        }



        //if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > 0)

        if (Input.GetKeyDown(KeyCode.M))
        {
            Database.releaseKite = true;
        }

        //if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y == 0)

        if(Input.GetKeyUp(KeyCode.M))
        {
            Database.releaseKite = false;
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "rayArea")
        {
            EnterArea = true;

        }

    }



}
