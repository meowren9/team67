using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLookAt : MonoBehaviour {

    public Transform target;
    public Transform head;
    public float speed;

    private void Start()
    {
        //head.eulerAngles += new Vector3(-9.211f, 90, 0);
        transform.forward = Vector3.left;
        //transform.eulerAngles += new Vector3(0, 90, 0);
    }

    void Update () {
        var targetRotation = Quaternion.LookRotation(target.position - transform.position);
        //TODO: need constraint
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        //transform.rotation += Quaternion()
        //head.eulerAngles += new Vector3(0, 90, 0);

    }
}
