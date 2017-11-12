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

            Line.positionCount = 3;
            Line.SetPosition(0, playerSky.position);
            Line.SetPosition(1, handRight.position);
            Line.SetPosition(2, kiteKit.position);
            
        }

        else
        {
            if (!added)
            {
                print("Called");
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
                playerSky.position += new Vector3(0.0f, 0.0f, -0.02f);

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
                playerSky.position += new Vector3(0.0f, 0.0f, 0.02f);


            }

            else
            {
                added = false;
                Destroy(GameObject.Find("LineCollider"), 0.1f);
            }
        }


        if (Database.releaseKite)
        {
            playerSky.position += new Vector3(-0.02f, 0.02f, 0.0f);
        }



        if (Database.isPull)
        {
            playerSky.position += new Vector3(0.0f, -0.02f, 0.0f);
        }

        if (Database.dragBack)
        {
            playerSky.position += new Vector3(0.002f, 0.0f, 0.0f);
        }
        


        
    }

    IEnumerator DelayFollow()
    {
        yield return new WaitForSeconds(1.0f);
        Database.startFollow = true;
    }


   



}
