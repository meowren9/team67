using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour
{

    public GameObject p1ovr;
    public GameObject p2ovr;

    public GameObject p1Ava;
    public GameObject p2Ava;

    public bool define_debug;
    public static bool debug = true;
    public int player = 2;
    public GameObject firework;

    //test
    public GameObject cube;
    public GameObject p1;

    void Start()
    {

        debug = define_debug;
           
            if (GameManager.debug)
            {
                if(player == 1)
                {
                    p1ovr.SetActive(true);
                    p2ovr.SetActive(false);
                    p1Ava.SetActive(false);
                    p2Ava.SetActive(true);

            }
            else
                {
                    p1ovr.SetActive(false);
                    p2ovr.SetActive(true);
                    p1Ava.SetActive(true);
                    p2Ava.SetActive(false);
            }
                
            }
            else
            {
                if (PhotonNetwork.isMasterClient) //p1
                {
                    p1ovr.SetActive(true);
                    p2ovr.SetActive(false);
                    p1Ava.SetActive(false);
                    p2Ava.SetActive(true);
            }
                else //p2
                {
                    p1ovr.SetActive(false);
                    p2ovr.SetActive(true);
                    p1Ava.SetActive(true);
                    p2Ava.SetActive(false);
            }
            }



        }


}