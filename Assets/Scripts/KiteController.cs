using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteController : MonoBehaviour {



    LineRenderer Line;

    public Transform playerSky;
    public Transform kiteKit;
    public Transform handRight;

    public float highest = 35.0f;
    public float lowest = 6.0f;
    public float xMax = 16.0f;
    public float xMin = -7.0f;
    public float zMax = 28.0f;
    public float zMin = -8.0f;
    Vector3 kiteDirection;

    bool added = false;

    void Start()
    {
        Line = GameObject.Find("line").GetComponent<LineRenderer>();


    }

    void Update () {

        kiteDirection = playerSky.position - kiteKit.position;


        if(Database.finishFight == true)
        {

            lowest = 0.52f;
            xMin = -12.95f;
            float dist = Vector3.Distance(playerSky.position, kiteKit.position);
            if(dist < 1.0f)
            {
                //near enough
            }

        }

        if (Database.isTutorial == true)
        {

            lowest = 0.52f;
            xMin = -12.95f;
            float dist = Vector3.Distance(playerSky.position, kiteKit.position);
            if (dist < 1.0f)
            {
                //near enough
            }

        }

        if (Database.handDrag)
        {

            Line.positionCount = 3;
            Line.SetPosition(0, playerSky.position);
            Line.SetPosition(1, handRight.position);
            Line.SetPosition(2, kiteKit.position);
            
        }

        else
        {
            Line.positionCount = 2;
            Line.SetPosition(0, playerSky.position);
            Line.SetPosition(1, kiteKit.position);
        }


        if (Database.dragLeft)
        {
            if (Database.handDrag)
            {
                if(playerSky.position.z+0.02 < zMax)
                    playerSky.position += new Vector3(0.0f, 0.0f, 0.02f);
            }
            
        }

        if (Database.dragRight)
        {
            if (Database.handDrag)
            {
                if (playerSky.position.z-0.02 > zMin)
                    playerSky.position += new Vector3(0.0f, 0.0f, -0.02f);
            }
        }


        if (Database.dragBack)
        {
            if (Database.handDrag)
            {
                if (playerSky.position.x-0.02 > xMin)
                    playerSky.position += new Vector3(-0.02f, 0.0f, 0.0f);
            }
           
        }

        if (Database.dragForward)
        {
            if (Database.handDrag)
            {
                if (playerSky.position.x + 0.02 < xMax)
                    playerSky.position += new Vector3(0.02f, 0.0f, 0.0f);
            }
        }



        if (Database.releaseKite)
        {
            if (
                playerSky.position.y + kiteDirection.y * 0.003f < highest 
                && playerSky.position.x + kiteDirection.x * 0.003f < xMax 
                && playerSky.position.z - kiteDirection.z * 0.003f > zMin 
                && playerSky.position.z + kiteDirection.z * 0.003f < zMax)
            {
                print("release");
                playerSky.position += kiteDirection * 0.003f;
            }



        }

        if (Database.isPull)
        {

            if (playerSky.position.y - kiteDirection.y * 0.003f > lowest
                && playerSky.position.x - kiteDirection.x * 0.003f > xMin)
            {

                playerSky.position -= kiteDirection * 0.003f;
            }


        }

    }



}
