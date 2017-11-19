﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NianHealth : Photon.PunBehaviour, IPunObservable
{

    public int status = 0; //0:normal 1:angry 2:dead
    [SerializeField] int hit_count = 0;

    public int change_point = 0;

    public int health = 0;

    public ParticleSystem particle;
    public Detection danger;

    bool explode = false;

    int AnalyseStatus()
    {
        if(hit_count < change_point)
        {
            return 0;
        }

        if(hit_count >= change_point && hit_count < health)
        {
            return 1;
        }

        if(hit_count >= health)
        {
            return 2;
        }

        Debug.Log("NianHealth: Strange Health...");
        return -1;
    }

    void Update()
    {
        if(GameManager.debug)
        {
            if(Input.GetKeyDown(KeyCode.H))
            {
                //particle.SetActive(false);
                //particle.SetActive(true);
                StartCoroutine(Explode());
                hit_count++;
                status = AnalyseStatus();
                danger.isDetected = false;
            }
        }

        var emission = particle.emission;
        emission.enabled = explode;
    }

    //public float p_len;

    

    IEnumerator Explode()
    {
        explode = false;
        yield return null;
        explode = true;
        yield return new WaitForSeconds(3f);
        explode = false;
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(!PhotonNetwork.isMasterClient || GameManager.debug))
            return;


        if(other.tag == "firework")
        {
            var firework = other.gameObject.GetComponent<SetFire>();
            if(firework.fired)
            {
                //particle.SetActive(false);
                //particle.SetActive(true);
                StartCoroutine(Explode());

                hit_count++;
                status = AnalyseStatus();
               
                Destroy(other.gameObject);
                //Debug.Log("danger change detected...");
                danger.isDetected = false;

            }
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(status);
            stream.SendNext(explode);
        }
        else
        {
            this.status = (int)stream.ReceiveNext();
            this.explode = (bool)stream.ReceiveNext();
        }
    }

}
