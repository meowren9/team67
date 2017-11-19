using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFire : MonoBehaviour {


    public GameObject Sparkle;
    public bool fired = false;
    public GameObject firedSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "fireLantern")
        {
            Sparkle.SetActive(true);
            fired = true;
            firedSound.SetActive(true);
        }
    }
}
