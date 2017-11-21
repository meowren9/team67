using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPCManager : Photon.MonoBehaviour {

    public GameObject firework_ending;
    public GameObject firework_set;

    public AudioClip[] sounds;
    AudioSource my_audio;
    PhotonView photonview;

    public AudioSource nianroar;
    public AudioSource special;
    public AudioSource normal;


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

    public void Roar()
    {
        photonView.RPC("NetworkRoar", PhotonTargets.All);
    }

    public void Normal()
    {
        photonView.RPC("NetworkNormal", PhotonTargets.All);
    }

    public void Special()
    {
        photonView.RPC("NetworkSpecial", PhotonTargets.All);
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
    void NetworkRoar()
    {
        nianroar.Play();
    }

    [PunRPC]
    void NetworkSpecial()
    {
        special.Play();
    }

    [PunRPC]
    void NetworkNormal()
    {
        normal.Play();
    }


    [PunRPC]
    void NetworkSetFirework()
    {
        firework_ending.SetActive(true);
        firework_set.SetActive(true);
    }

    [PunRPC]
    void NetworkPlaySound(int index)
    {
        my_audio.Stop();
        my_audio.PlayOneShot(sounds[index]);
    }

}
