using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarControl : MonoBehaviour {

    AudioSource roar;
	// Use this for initialization
	void Start () {

        roar = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {

        playMusic();

    }

    IEnumerator DelayRoar()
    {
        print("in");
        yield return new WaitForSeconds(3.0f);
        roar.Play();
        
    }


    void playMusic()
    {
        StartCoroutine(DelayRoar());

    }
}
