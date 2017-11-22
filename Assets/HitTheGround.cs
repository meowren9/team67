using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTheGround : MonoBehaviour {


    public GameObject particle;
    public GameObject firework;
    public AudioClip hit_ground_sound;
    AudioSource my_audio;
    PhotonView photonView;

    private void Start()
    {
        my_audio = GetComponent<AudioSource>();
        photonView = GetComponent<PhotonView>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ground")
        {
            if (GetComponent<SetFire>().fired)
            {
                print("touch ground");

                if (!PhotonNetwork.isMasterClient)
                    photonView.RPC("NetworkHitGroundSound", PhotonTargets.All);

                particle.SetActive(true);
                this.GetComponent<SetFire>().firedSound.SetActive(false);
            }

            //if(GameManager.debug)
                Destroy(this.gameObject, 3.0f);
            //else
            //{
            //    PhotonNetwork.Destroy(this.gameObject);
            //}
        }
    }

    [PunRPC]
    void NetworkHitGroundSound()
    {
        my_audio.Stop();
        my_audio.PlayOneShot(hit_ground_sound);
    }

}
