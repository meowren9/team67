using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {

    public AudioClip[] bgms;
    AudioSource my_audio;
    public GameManager gm;

    int prev_gm_status;

    private void Start()
    {
        prev_gm_status = gm.status;
        my_audio = GetComponent<AudioSource>();
        my_audio.clip = bgms[0];
        my_audio.Play();
        prev_gm_status = gm.status;
    }


    private void Update()
    {
        int cur = gm.status;
        if(cur != prev_gm_status)
        {
            PlayMusic(cur);
            prev_gm_status = cur;
        }
    }

    void PlayMusic(int index)
    {
        my_audio.Stop();
        my_audio.clip = bgms[index];
        my_audio.Play();
    }
}
