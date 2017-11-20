using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPCManager : Photon.MonoBehaviour {

    public GameObject firework_ending;
    public AudioClip[] sounds;
    AudioSource my_audio;
    PhotonView photonview;

    void Start()
    {
        photonview = GetComponent<PhotonView>();
        my_audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                SetFirework();
            }
        }

    }

    public void SetFirework()
    {
        photonView.RPC("NetworkSetFirework", PhotonTargets.All);
    }

    public void PlaySound(int index)
    {
        photonView.RPC("NetworkPlaySound", PhotonTargets.All, index);
    }


    [PunRPC]
    void NetworkSetFirework()
    {
        firework_ending.SetActive(true);
    }

    [PunRPC]
    void NetworkPlaySound(int index)
    {
        my_audio.Stop();
        my_audio.PlayOneShot(sounds[index]);
    }

}
