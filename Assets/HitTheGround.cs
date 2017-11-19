using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTheGround : MonoBehaviour {


    public GameObject particle;
    public GameObject firework;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ground")
        {
            if (GetComponent<SetFire>().fired)
            {
                print("touch ground");
                particle.SetActive(true);
            }

            Destroy(this.gameObject, 3.0f);
        }
    }
}
