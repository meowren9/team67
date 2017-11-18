using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHandle : MonoBehaviour {

    LineRenderer Line;
    public GameObject handlePoint;
    public GameObject flower;
    // Use this for initialization
    void Start () {
        Line = GameObject.Find("line_2").GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        
        this.transform.position = new Vector3(handlePoint.transform.position.x, handlePoint.transform.position.y - 0.2f, handlePoint.transform.position.z);
        Line.positionCount = 2;
        Line.SetPosition(0, handlePoint.transform.position);
        Line.SetPosition(1, flower.transform.position);
    }
}
