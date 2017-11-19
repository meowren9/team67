using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NianHealth : MonoBehaviour {

    public int status = 0; //0:normal 1:angry 2:dead
    int hit_count = 0;

    public int change_point = 0;

    public int health = 0;

    public GameObject particle;


    int AnalyseStatus()
    {
        if(hit_count < change_point)
        {
            return 0;
        }

        if(hit_count >= change_point && hit_count < health)
        {
            return 1;
        }

        if(hit_count >= health)
        {
            return 2;
        }

        Debug.Log("NianHealth: Strange Health...");
        return -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "firework")
        {
            var firework = other.gameObject.GetComponent<SetFire>();
            if(firework.fired)
            {
                particle.SetActive(false);
                particle.SetActive(true);
                //Destroy firework
                //Set particle
                hit_count++;
                status = AnalyseStatus();
                Destroy(other.gameObject,0.1f);
            }
        }
    }

}
