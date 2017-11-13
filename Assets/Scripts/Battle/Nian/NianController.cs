using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NianController : Photon.PunBehaviour, IPunObservable { //controled in master
    

    //attack
    public float attackCD;
    public float beamLastTime;
    bool IsFiring;
    public ParticleSystem Beams;

    //target
    bool alive;
    public int health = 5;
    public int active_target_idx = 0;
    public GameObject[] targets;

    AudioSource nian_audio;

    void Awake()
    {
        nian_audio = GetComponent<AudioSource>();
        IsFiring = false;
        alive = true;
        if (PhotonNetwork.isMasterClient || GameManager.debug)
        {
            StartCoroutine(Attack());
        }
    }

    private void Start()
    {
        
    }

    bool prev_fire = false;

    void Update()
    {
        if(prev_fire == false && IsFiring == true) 
        {
            nian_audio.Play();
        }
        prev_fire = IsFiring;

        var emission = Beams.emission;
        emission.enabled = IsFiring;

        
        


        for (int i = 0; i < health; i++)
        {
            if (i == active_target_idx)
            {
                targets[i].SetActive(true);
            }
            else
            {
                targets[i].SetActive(false);
            }
        }
    }


    public void ChangeTarget()
    {
        active_target_idx ++;
        if (active_target_idx == health)
            alive = false;
    }

    IEnumerator Attack()
    {
        //yield return new WaitForSeconds(3f);
        while (alive)
        {
            //TODO: particle emission
            IsFiring = true;
            yield return new WaitForSeconds(beamLastTime);
            IsFiring = false;
            yield return new WaitForSeconds(attackCD);
        }
        
        //TODO: RUN AWAY
        yield  break;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(IsFiring);
            stream.SendNext(active_target_idx);
            stream.SendNext(alive);
        }
        else
        {
            // Network player, receive data
            this.IsFiring = (bool)stream.ReceiveNext();
            this.active_target_idx = (int)stream.ReceiveNext();
            this.alive = (bool)stream.ReceiveNext();
        }
    }
}
