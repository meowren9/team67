using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour
{

    public GameObject p1ovr;
    public GameObject p2ovr;
    //public GameObject hand1;



    public static bool debug = true;
    public GameObject firework;

    //test
    public GameObject cube;
    public GameObject p1;

    void Start()
    {
        //fire work
        //if (firework == null)
        //{
        //    Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        //}
        //else
        //{
        //    //instantiate firework and basket
        //    if (!PhotonNetwork.isMasterClient)
        //    {
        //        //TODO
        //        GameObject f = PhotonNetwork.Instantiate(this.firework.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);

        //    }

        //}

            
           

            if (GameManager.debug)
            {
                p1ovr.SetActive(true);
                p2ovr.SetActive(false);
            }
            else
            {
                if (PhotonNetwork.isMasterClient) //p1
                {
                    p1ovr.SetActive(true);
                    p2ovr.SetActive(false);
                }
                else //p2
                {
                    p1ovr.SetActive(false);
                    p2ovr.SetActive(true);
                }
            }



        }


}