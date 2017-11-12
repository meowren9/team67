using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public bool active = true;
    public NianController nian;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "firework" && active)
        {
            active = false;
            nian.ChangeTarget();
            Debug.Log("enter");
        }
    }
}
