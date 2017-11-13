using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPosition : MonoBehaviour {



    public Transform P2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //if(PhotonNetwork.isMasterClient)
            this.transform.position = P2.position;
	}
}
