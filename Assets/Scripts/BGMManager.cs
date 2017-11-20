using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour, IPunObservable
{
    public int current_bgm_index = 0;
    public AudioClip[] bgms;
    AudioSource my_audio;

    private void Update()
    {
        PlayMusic();
    }

    void PlayMusic()
    {
        if (my_audio.clip == bgms[current_bgm_index])
            return;

        my_audio.Stop();
        my_audio.clip = bgms[current_bgm_index];
        my_audio.Play();
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(current_bgm_index);
        }
        else
        {
            this.current_bgm_index = (int)stream.ReceiveNext();
        }
    }
}
