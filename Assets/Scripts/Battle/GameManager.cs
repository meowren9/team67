using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour,IPunObservable
{
    //game status
    public int status = 0;
    //0: tutorial
    //1: battle - 1
    //2: battle -2
    //3: ending

    public bool ready = false;
    //public static bool nian_angry = false;
    NianHealth health;
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


    //bgm
    public GameObject bgm_prefab;
    BGMManager bgm_manager;

    //rpc
    public RPCManager rpc_manager;

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

            
            bgm_manager = Instantiate(bgm_prefab).GetComponent<BGMManager>();
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
                
                bgm_manager = PhotonNetwork.Instantiate(bgm_prefab.name,Vector3.zero,Quaternion.identity,0).GetComponent<BGMManager>();
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
                    if (GameManager.debug)
                    {
                        nian = Instantiate(nian_prefab, appearPoint.position, appearPoint.rotation);
                        health = nian.GetComponent<NianAI>().health;

                    }
                    else
                    {
                        nian = PhotonNetwork.Instantiate(nian_prefab.name, appearPoint.position, appearPoint.rotation, 0);
                        health = nian.GetComponent<NianAI>().health;
                    }
                    StartCoroutine(PrepareNian());
                    //bgm_manager.current_bgm_index = 1;
                    status = 1;
                    
                }
                break;
            case 1:
                if (health.status == 1)
                {
                    //nian roar!!!
                    rpc_manager.DestroyBasket();
                    villagers.SetActive(true);

                    //villager help
                    status = 2;
                    bgm_manager.current_bgm_index = status;
                }
                break;

            case 2:
                if(nian_defeat)
                {
                    //ending scene
                    rpc_manager.ShowEnding();
                    rpc_manager.SetFirework();
                    status = 3;
                    bgm_manager.current_bgm_index = status;
                }
                break;
        }
    }

    //IEnumerator 

    IEnumerator PrepareNian()
    {
        //nian.SetActive(true);
        bgm_manager.StopMusic();
        rpc_manager.PlaySound(0);
        yield return new WaitForSeconds(3f);
        bgm_manager.current_bgm_index = status;

        Vector3 targetPosition = new Vector3(showPoint.position.x, nian.transform.position.y, showPoint.position.z);

        Debug.Log("roar");

        while (Vector3.Distance(targetPosition,nian.transform.position) > 0.3f)
        {
            nian.transform.position = Vector3.Lerp(nian.transform.position, targetPosition, Time.deltaTime * showSpeed);
            yield return null;
        }

        //roar
       
        yield return new WaitForSeconds(2f);
        //villiager disapear
        Debug.Log("villiager disapear");

        nian.GetComponent<NianAI>().enabled = true;

        // game start
        yield break;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(status);
        }
        else
        {
            this.status = (int)stream.ReceiveNext();
        }
    }

}