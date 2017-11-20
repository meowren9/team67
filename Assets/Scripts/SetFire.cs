using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFire : Photon.PunBehaviour, IPunObservable
{

    public GameObject Sparkle;
    public bool fired = false;
    public GameObject firedSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "fireLantern" && !PhotonNetwork.isMasterClient)
        {
            Sparkle.SetActive(true);
            fired = true;
            //firedSound.SetActive(true);
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(fired);
        }
        else
        {
            this.fired = (bool)stream.ReceiveNext();
        }
    }
}
