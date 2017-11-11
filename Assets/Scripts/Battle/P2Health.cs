using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Health : Photon.PunBehaviour, IPunObservable { //controlled by master

    public float health = 100f;
    public float heartSpeed = 3f;

    void OnParticleCollision(GameObject other)
    {
            if (PhotonNetwork.isMasterClient ||  GameManager.debug)
            {
                if (health > 0)
                    health -= heartSpeed * Time.deltaTime;
            }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            this.health = (float)stream.ReceiveNext();
        }
    }
}
