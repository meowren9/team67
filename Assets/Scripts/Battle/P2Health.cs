using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P2Health : Photon.PunBehaviour, IPunObservable { //controlled by master

    public float health = 100f;
    public float heartSpeed = 3f;

    public GameObject Blood;
    Image BloodImage;

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


    void Update()
    {

        BloodImage = Blood.GetComponent<Image>();
        BloodImage.color = new Color(155.0f, 2.0f, 2.0f, 1-health/100);

    }
}
