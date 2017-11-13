using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public bool active = true;
    public NianController nian;
    public GameObject fireworkParticle;

    public GameObject fireworkSet;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "firework" && active)
        {
            active = false;

            fireworkParticle.SetActive(true);
            nian.ChangeTarget();
            Debug.Log("enter");
            
            //finish the last target
            if(this.name == "Face")
            {
                fireworkSet.SetActive(true);
            }
        }
    }
}
