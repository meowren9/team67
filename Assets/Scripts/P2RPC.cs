using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2RPC : Photon.MonoBehaviour {

    // Use this for initialization
    PhotonView photonview;

	void Start () {
        //photonview = GetComponent<PhotonView>();
        photonview = GameObject.Find("RPCManager").GetComponent<PhotonView>();
    }
	
	void Update () {
        if(!PhotonNetwork.isMasterClient||true)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                SetFirework();
            }
        }
		
	}

    void SetFirework()
    {
        photonView.RPC("NetworkSetFirework", PhotonTargets.All);
    }
}
