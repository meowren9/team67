using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NianController : Photon.PunBehaviour, IPunObservable { //controled in master

    public float attackCD;
    public float beamLastTime;
    public int health;
    bool IsFiring;
    public ParticleSystem Beams;
    
    void Awake()
    {
        IsFiring = false;
        
        if(PhotonNetwork.isMasterClient || GameManager.debug)
            StartCoroutine(Attack());

    }

    void Update()
    {
        var emission = Beams.emission;
        emission.enabled = IsFiring;
    }

    IEnumerator Attack()
    {
        //yield return new WaitForSeconds(3f);
        while (health > 0)
        {
            //TODO: particle emission
            IsFiring = true;
            yield return new WaitForSeconds(beamLastTime);
            IsFiring = false;
            yield return new WaitForSeconds(attackCD);
        }

        yield  break;
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(IsFiring);
            //stream.SendNext(health);
        }
        else
        {
            // Network player, receive data
            this.IsFiring = (bool)stream.ReceiveNext();
            //this.health = (int)stream.ReceiveNext();
        }
    }
}
