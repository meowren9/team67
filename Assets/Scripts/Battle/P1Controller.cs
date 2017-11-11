using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : MonoBehaviour {

    public GameObject kite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.debug)
        {
            if (!PhotonNetwork.isMasterClient) //p1
            {
                return;
            }
        }
        //input
        if (Input.GetKey(KeyCode.UpArrow))
        { 
            kite.transform.position += Vector3.up * 0.5f * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            kite.transform.position += Vector3.down * 0.5f * Time.deltaTime;
        }

    }
}
