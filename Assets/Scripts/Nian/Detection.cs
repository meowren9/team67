using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {

    public bool isDetected = false;
    public int mode = 0;
    //0 - attack
    //1 - danger

    //2 - targer ???


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger stay");
        if(other.tag == "Player" && mode == 0)
        {
            isDetected = true;
        }
        if(other.tag == "firework" && mode ==1)
        {
            if (other.gameObject != null)
            {
                isDetected = true;
                print(other.name);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("trigger exit");
        if (other.tag == "Player" && mode == 0)
        {
             //Debug.Log("trigger exit");
            isDetected = false;
        }
        if (other.tag == "firework" && mode == 1)
        {
            isDetected = false;
        }
    }



}
