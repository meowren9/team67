using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour
{
    //game status
    public int status = 0;
    //0: tutorial
    //1: battle - 1
    //2: battle -2
    //3: ending

    public bool ready = false;
    //public static bool nian_angry = false;
    public NianHealth health;
    public static bool nian_defeat = false;
    //public NianAI nian_ai;

    public GameObject villagers;

    //nian
    public GameObject nian;
    public GameObject nian_prefab;
    public Transform appearPoint;
    public Transform showPoint;
    public float showSpeed = 0f;

    //player
    public GameObject p1ovr;
    public GameObject p2ovr;
    public GameObject p1Ava;
    public GameObject p2Ava;

    //lantern
    public GameObject lotus;

    //debug
    public bool define_debug;
    public static bool debug = true;
    public int player = 2;


    void Start()
    {

        debug = define_debug;
        if (GameManager.debug)
        {
            if (player == 1)
            {
                p1ovr.SetActive(true);
                p2ovr.SetActive(false);
                p1Ava.SetActive(false);
                p2Ava.SetActive(true);
                lotus.SetActive(false);

            }
            else
            {
                p1ovr.SetActive(false);
                p2ovr.SetActive(true);
                p1Ava.SetActive(true);
                p2Ava.SetActive(false);
                lotus.SetActive(true);
            }

            nian = Instantiate(nian_prefab, appearPoint.position, appearPoint.rotation);
        }
        else
        {
            if (PhotonNetwork.isMasterClient) //p1
            {
                p1ovr.SetActive(true);
                p2ovr.SetActive(false);
                p1Ava.SetActive(false);
                p2Ava.SetActive(true);
                lotus.SetActive(false);
            }
            else //p2
            {
                p1ovr.SetActive(false);
                p2ovr.SetActive(true);
                p1Ava.SetActive(true);
                p2Ava.SetActive(false);
                lotus.SetActive(true);
                nian = PhotonNetwork.Instantiate(nian_prefab.name, appearPoint.position, appearPoint.rotation, 0);
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ready = true;
        }

        if (!(!PhotonNetwork.isMasterClient || GameManager.debug)) //p2 control
            return;

        switch (status)
        {
            case 0:
                if (ready)
                {
                    StartCoroutine(PrepareNian());
                    status = 1;
                }
                break;
            case 1:
                if (health.status == 1)
                {
                    villagers.SetActive(true);
                    status = 2;
                }
                break;

            case 2:
                if(nian_defeat)
                {
                    //ending scene
                    status = 3;
                }
                break;
        }
    }

    IEnumerator PrepareNian()
    {
        nian.SetActive(true);
        Vector3 targetPosition = new Vector3(showPoint.position.x, nian.transform.position.y, showPoint.position.z);

        while(Vector3.Distance(targetPosition,nian.transform.position) > 0.3f)
        {
            nian.transform.position = Vector3.Lerp(nian.transform.position, targetPosition, Time.deltaTime * showSpeed);
            yield return null;
        }

        //roar
        Debug.Log("roar");
        yield return new WaitForSeconds(2f);
        //villiager disapear
        Debug.Log("villiager disapear");

        nian.GetComponent<NianAI>().enabled = true;

        // game start
        yield break;
    }

}