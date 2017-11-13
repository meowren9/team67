﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiteController : MonoBehaviour {



    LineRenderer Line;

    public Transform playerSky;
    public Transform kiteKit;
    public Transform handRight;

    public float highest = 35.0f;
    public float lowest = 12.0f;
    Vector3 kiteDirection;

    bool added = false;
    void Start()
    {
        Line = GameObject.Find("line").GetComponent<LineRenderer>();


    }

    void Update () {

        kiteDirection = playerSky.position - kiteKit.position;

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
                print("Left!");
                playerSky.position += new Vector3(0.0f, 0.0f, 0.02f);
            }
            
        }

        if (Database.dragRight)
        {
            if (Database.handDrag)
            {
                print("Right!");
                playerSky.position += new Vector3(0.0f, 0.0f, -0.02f);
            }
        }


        if (Database.dragBack)
        {
            if (Database.handDrag)
            {
                print("back!");
                playerSky.position += new Vector3(-0.02f, 0.0f, 0.0f);
            }
           
        }

        if (Database.dragForward)
        {
            if (Database.handDrag)
            {
                print("Forward!");
                playerSky.position += new Vector3(0.02f, 0.0f, 0.0f);
            }
        }



        if (Database.releaseKite)
        {
            if(playerSky.position.y < highest)
            {
                playerSky.position += kiteDirection * 0.001f;
            }
            else
            {
                print("out of range");
                playerSky.position += new Vector3(kiteDirection.x * 0.001f, 0.0f, kiteDirection.z * 0.001f);
            }
            
        }

        if (Database.isPull)
        {

            if(playerSky.position.y > lowest)
            {
                playerSky.position -= kiteDirection * 0.001f;
            }
            else
            {
                print("out of range");
                playerSky.position -= new Vector3 (kiteDirection.x * 0.001f, 0.0f, kiteDirection.z * 0.001f);
                
            }
            
        }

    }



}
