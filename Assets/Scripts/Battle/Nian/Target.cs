using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public bool active = true;
    public NianController nian;
    public GameObject fireworkParticle;



    //sijie
    public GameObject fireworkSet;
    public GameObject lanternSet;


    bool moveAway = false;
    //sijie end
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "firework" && active)
        {
            active = false;

            fireworkParticle.SetActive(true);
            nian.ChangeTarget();

            



            //Sijie 
            //finish the last target
            if(this.name == "Face")
            {
                fireworkSet.SetActive(true);
                
                
            }

            else if (this.name == "Back")
            {
                lanternSet.SetActive(true);
            }


            //Sijie End
        }
    }

}
