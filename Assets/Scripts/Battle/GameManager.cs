using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour {

    public GameObject cam1;
    public GameObject cam2;
    public GameObject hand1;

    public static bool debug = true;

	// Use this for initialization
	void Start () {
        //TODO: choose ovr

        if (GameManager.debug)
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
            hand1.SetActive(true);
        }
        else
        {
            if (PhotonNetwork.isMasterClient) //p1
            {
                cam1.SetActive(true);
                hand1.SetActive(true);
                cam2.SetActive(false);
            }
            else //p2
            {
                cam1.SetActive(false);
                hand1.SetActive(false);
                cam2.SetActive(true);
            }
        }
        

    }
	
}
