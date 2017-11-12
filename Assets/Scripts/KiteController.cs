using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteController : MonoBehaviour {



    LineRenderer Line;

    public Transform playerSky;
    public Transform kiteKit;
    public Transform handRight;


    bool added = false;
    void Start()
    {
        Line = GameObject.Find("line").GetComponent<LineRenderer>();
        


    }

    void Update () {





        if (Database.handDrag)
        {
            print("handdrag = true");
            Line.positionCount = 3;
            Line.SetPosition(0, playerSky.position);
            Line.SetPosition(1, handRight.position);
            Line.SetPosition(2, kiteKit.position);
            
        }

        else
        {
            if (!added)
            {
                
                addColliderToLine();
                added = true;
            }
            
            

            Line.positionCount = 2;
            Line.SetPosition(0, playerSky.position);
            Line.SetPosition(1, kiteKit.position);
        }


        if (Database.dragLeft)
        {
            if (Database.handDrag)
            {
                playerSky.position += new Vector3(0.0f, 0.0f, -0.1f);

            }

            else
            {
                added = false;
                Destroy(GameObject.Find("LineCollider"), 0.1f);
            }
            
        }

        if (Database.dragRight)
        {
            if (Database.handDrag)
            {
                playerSky.position += new Vector3(0.0f, 0.0f, 0.1f);


            }

            else
            {
                added = false;
                Destroy(GameObject.Find("LineCollider"), 0.1f);
            }
        }

        if (Database.dragBack)
        {
            if (Database.handDrag)
            {
                playerSky.position += new Vector3(0.01f, -0.01f, 0.0f);


            }

        }

        if (Database.releaseKite)
        {
            playerSky.position += new Vector3(-0.1f, 0.1f, 0.0f);
        }



        if (Database.isPull)
        {
            if (!Database.handDrag)
            {
                playerSky.position += new Vector3(0.002f, -0.002f, 0.0f);

            }
        }


        


        
    }

    IEnumerator DelayFollow()
    {
        yield return new WaitForSeconds(1.0f);
        Database.startFollow = true;
    }


    private void addColliderToLine()
    {
        BoxCollider col = new GameObject("LineCollider").AddComponent<BoxCollider>();
        col.transform.parent = Line.transform; // Collider is added as child object of line
        float lineLength = Vector3.Distance(playerSky.position, kiteKit.position); // length of line
        col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        Vector3 midPoint = (playerSky.position + kiteKit.position) / 2;
        col.transform.position = midPoint; // setting position of collider object
        // Following lines calculate the angle between playerSky.position and kiteKit.position
        float angle = (Mathf.Abs(playerSky.position.y - kiteKit.position.y) / Mathf.Abs(playerSky.position.x - kiteKit.position.x));
        if ((playerSky.position.y < kiteKit.position.y && playerSky.position.x > kiteKit.position.x) || (kiteKit.position.y < playerSky.position.y && kiteKit.position.x > playerSky.position.x))
        {
            angle *= -1;
        }
        angle = Mathf.Rad2Deg * Mathf.Atan(angle);
        col.transform.Rotate(0, 0, angle);
    }



}
