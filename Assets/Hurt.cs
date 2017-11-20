using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("collide");
        if(other.tag == "claw")
        {
            print("inclaw");
            Database.hurt = true;
        }
    }
}
