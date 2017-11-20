using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : MonoBehaviour {

    Animator anim;

    public Transform inside;
    public Transform outside;
    public Transform inTheMiddle;
    public Transform lanternPosition;
    public Transform peoplePosition;

    public float speedLantern = 0.2f;
    public float speedVillager = 0.5f;
    public float speedLanternUp = 0.5f;

    bool villagerGone = false;
    bool releaseLantern = false;
    GameObject lantern;
    GameObject people;
    GameObject duanbianpao;

    public GameObject lantern_prefab;
    public GameObject people_prefab;
    public GameObject duanbianpao_prefab;
    

    float t = 0.0f;
    public float minimum = -1.0f;
    public float maximum = 1.0f;

    float randNum;
    // Use this for initialization
    void Start () {
       
        if (!(!PhotonNetwork.isMasterClient || GameManager.debug))
            return;

        randNum = Random.Range(1, 20);

        if (GameManager.debug)
        {
            lantern = Instantiate(lantern_prefab, lanternPosition.position,lanternPosition.rotation);
            //lantern.transform.parent = this.transform;
            lantern.GetComponent<FollowParent>().parent = this.gameObject;

            people = Instantiate(people_prefab, peoplePosition.position, peoplePosition.rotation);
            people.transform.forward = -people.transform.forward;
            //people.transform.parent = this.transform;
            people.GetComponent<FollowParent>().parent = this.gameObject;

            anim = people.GetComponent<Animator>();
            duanbianpao = Instantiate(duanbianpao_prefab, lanternPosition.position, lanternPosition.rotation);
            //duanbianpao.transform.parent = lantern.transform;
            duanbianpao.GetComponent<FollowParent>().parent = lantern;

        }
        else
        {
            if(!PhotonNetwork.isMasterClient)
            {
                lantern = PhotonNetwork.Instantiate(lantern_prefab.name, lanternPosition.position, lanternPosition.rotation,0);
                //PhotonNetwork.Instantiate()

                //lantern.GetComponent<FollowParent>().parent = this.gameObject;
                //lantern.transform.parent = this.transform;

                people = PhotonNetwork.Instantiate(people_prefab.name, peoplePosition.position, peoplePosition.rotation,0);
                people.transform.forward = -people.transform.forward;
                //people.transform.parent = this.transform;
                //people.GetComponent<FollowParent>().parent = this.gameObject;

                anim = people.GetComponent<Animator>();
                duanbianpao = PhotonNetwork.Instantiate(duanbianpao_prefab.name, lanternPosition.position, lanternPosition.rotation,0);
                //duanbianpao.transform.parent = lantern.transform;
                //duanbianpao.GetComponent<FollowParent>().parent = lantern;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (!(!PhotonNetwork.isMasterClient || GameManager.debug))
            return;

        if(t < 1.0f)
        {
            this.transform.position = Vector3.Lerp(inside.position, outside.position, t);
            t += speedVillager * Time.deltaTime * Random.Range(0.2f, 3.0f);
            
        }

        else
        {
            //if (!releaseLantern)
            //{
                StartCoroutine(ReleaseLight());
            //    releaseLantern = true;
            //}

        }
        

    }


    IEnumerator ReleaseLight()
    {
        if (!villagerGone)
        {
            if (!releaseLantern)
            {
                anim.SetTrigger("Release");
                releaseLantern = true;
            }
            
            yield return new WaitForSeconds(2.0f);
        }
        if (t > 1.0f && t < 2.0f)
        {
            lantern.transform.position = Vector3.Lerp(lanternPosition.position, new Vector3(inTheMiddle.position.x, inTheMiddle.position.y, inTheMiddle.position.z + randNum), t - 1);
            people.transform.position = Vector3.Lerp(peoplePosition.position, inside.position, t - 1);
            people.transform.LookAt(inside);
            t += speedVillager * Time.deltaTime * Random.Range(0.2f, 3.0f);
        }

        else if (t > 2.0f && t < 3.0f)
        {
            villagerGone = true;
            //Destroy(people, 0.1f);
            lantern.transform.position += new Vector3(0.0f, 0.4f * speedLanternUp * Random.Range(0.2f, 3.0f), 0.0f);
        }

    }


}
