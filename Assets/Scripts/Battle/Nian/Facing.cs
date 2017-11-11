using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facing : MonoBehaviour {
    public Transform target;
    public float speed;
    void Update()
    {
        Vector3 targerPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        var targetRotation = Quaternion.LookRotation(targerPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
