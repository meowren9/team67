using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {

    bool Flag = false;
    bool Flag2 = true;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.localPosition = new Vector3(transform.localPosition.x, 
            Mathf.PingPong(Time.time, 0.8f), transform.localPosition.z);



    }
}
