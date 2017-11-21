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

    public SpriteRenderer happy;
    public GameObject credit;

    public GameObject basket;


    void Start()
    {
        photonview = GetComponent<PhotonView>();
        my_audio = GetComponent<AudioSource>();
        //ShowEnding();
        //StartCoroutine(Ending());
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

    public void DestroyBasket()
    {
        photonView.RPC("NetworkDestroyBasket", PhotonTargets.All);
    }

    [PunRPC]
    void NetworkDestroyBasket()
    {
        basket.SetActive(false);
    }

    public void ShowEnding()
    {
        photonView.RPC("NetworkShowEnding", PhotonTargets.All);
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

    [PunRPC]
    void NetworkShowEnding()
    {
        StartCoroutine(Ending());
    }

    public float showSpeed;

    IEnumerator Ending()
    {
        credit.SetActive(true);
        while(happy.color.a < 1)
        {
            var color = happy.color;
            color.a = Mathf.Lerp(color.a, 1, Time.deltaTime * showSpeed);
            //Debug.Log(color.a);
            happy.color = color;
            yield return null;
        }

        yield break;
    }

}
