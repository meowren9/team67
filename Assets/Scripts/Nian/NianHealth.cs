using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NianHealth : Photon.PunBehaviour, IPunObservable
{

    public int status = 0; //0:normal 1:angry 2:dead
    [SerializeField] int hit_count = 0;

    public int change_point = 0;

    public int health = 0;

    public ParticleSystem particle;
    bool explode = false;

    public ParticleSystem s_particle;
    bool s_explode = false;



    public Detection danger;

    
    public ParticleSystem health_particle;

    public Animator nian_anim;


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

    void Update()
    {
        if(GameManager.debug || true)
        {
            if(Input.GetKeyDown(KeyCode.H))
            {
                //particle.SetActive(false);
                //particle.SetActive(true);
                StartCoroutine(Explode());
                nian_anim.SetTrigger("angry");
                hit_count++;
                status = AnalyseStatus();
                danger.isDetected = false;
            }
        }

        var emission = particle.emission;
        emission.enabled = explode;

        var s_emission = s_particle.emission;
        s_emission.enabled = s_explode;

        //health

        var remainHealth = health - hit_count + 5;
        var main = health_particle.main;
        main.startSize = remainHealth;

    }

    IEnumerator Explode()
    {
        explode = false;
        yield return null;
        explode = true;
        yield return new WaitForSeconds(2f);
        explode = false;
        yield break;
    }

    IEnumerator S_Explode()
    {
        s_explode = false;
        yield return null;
        s_explode = true;
        yield return new WaitForSeconds(3f);
        s_explode = false;
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(!PhotonNetwork.isMasterClient || GameManager.debug))
            return;


        if(other.tag == "firework")
        {
            var firework = other.gameObject.GetComponent<SetFire>();
            if(firework.fired)
            {
                //particle.SetActive(false);
                //particle.SetActive(true);
                StartCoroutine(Explode());

                nian_anim.SetTrigger("angry");

                hit_count++;
                status = AnalyseStatus();

                if (GameManager.debug)
                    Destroy(other.gameObject);
                else
                    PhotonNetwork.Destroy(other.gameObject);
                //Debug.Log("danger change detected...");
                danger.isDetected = false;

            }
        }


        if (other.tag == "SpecialFirework")
        {
            var firework = other.gameObject.GetComponent<SetFire>();
            if (firework.fired)
            {
                StartCoroutine(Explode());
                StartCoroutine(S_Explode());

                nian_anim.SetTrigger("angry");

                hit_count++;
                status = AnalyseStatus();

                if (GameManager.debug)
                    Destroy(other.gameObject);
                else
                    PhotonNetwork.Destroy(other.gameObject);

                //Debug.Log("danger change detected...");
                danger.isDetected = false;
            }
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(status);
            stream.SendNext(explode);
            stream.SendNext(s_explode);
            stream.SendNext(hit_count);
        }
        else
        {
            this.status = (int)stream.ReceiveNext();
            this.explode = (bool)stream.ReceiveNext();
            this.s_explode = (bool)stream.ReceiveNext();
            this.hit_count = (int)stream.ReceiveNext();
        }
    }

}
