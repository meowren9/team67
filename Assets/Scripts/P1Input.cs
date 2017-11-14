using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Input : MonoBehaviour
{


    bool EnterArea;
    bool pressTrigger;
    bool spaceDown;

    void Update()
    {



        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0)
        ////if (Input.GetKey(KeyCode.Space))
        {

            spaceDown = true;
        }

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 0)
        ////if (Input.GetKeyUp(KeyCode.Space))
        {

            spaceDown = false;
        }

        if (!GameManager.debug)
        {
            if (!PhotonNetwork.isMasterClient) //p1
            {
                return;
            }
        }


        if (Database.moveUp)
        {
            this.transform.position += new Vector3(-0.1f, 0.0f, 0.0f);
        }

        if (Database.moveDown)
        {
            this.transform.position += new Vector3(0.1f, -0.0f, 0.0f);
        }

        if (EnterArea && spaceDown)
        {
            Database.handDrag = true;
        }



        if (!spaceDown)
        {

            Database.handDrag = false;

        }



        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > 0)

        ////if (Input.GetKeyDown(KeyCode.M))
        {
            OVRInput.SetControllerVibration(1, 20, OVRInput.Controller.LTouch);
            Database.releaseKite = true;
        }

        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y == 0)

        ////if (Input.GetKeyUp(KeyCode.M))
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            Database.releaseKite = false;
        }




    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "rayArea")
        {
            EnterArea = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "rayArea")
        {
            EnterArea = false;

        }

    }

}
