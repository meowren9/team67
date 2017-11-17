using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkDestroy : MonoBehaviour {


    public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {


        if(other.tag == "firework")
        {

            Destroy(target, 0.1f);
            Database.tutorialTarget ++;
        }
    }
}
