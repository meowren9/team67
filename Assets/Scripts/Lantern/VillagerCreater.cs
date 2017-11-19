﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerCreater : MonoBehaviour {


    public GameObject[] VillagerWithLantern;

    public Transform[] presetPositon;
    public int VillagersPerUnitTime = 10;
    public float timeSlot = 3.0f;

	// Use this for initialization
	void Start () {
        StartCoroutine(CreateVillager());
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    IEnumerator CreateVillager()
    {
        while (true)
        {
            
            for (int i = 0; i < VillagersPerUnitTime; i++)
            {
                int PositionNumber = Random.Range(0, 21);
                int VillagerNumber = Random.Range(0, 2);
                GameObject.Instantiate(VillagerWithLantern[VillagerNumber], presetPositon[PositionNumber]);
            }
                


            yield return new WaitForSeconds(timeSlot);
        }
        
    }
}
