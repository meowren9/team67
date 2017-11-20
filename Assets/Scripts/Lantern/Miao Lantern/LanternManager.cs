using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternManager : MonoBehaviour {

    public Transform[] spawnLocations;
    public Transform center;

    public GameObject villagerPrefeb;
    public GameObject lanternPrefab;

    public float SpawnCD;

    void Start () {
        //GameManager.debug = false;
        if(GameManager.debug || !PhotonNetwork.isMasterClient)
        {
            StartCoroutine(Spawn());
        }
	}

    IEnumerator MoveLantern(GameObject lantern)
    {
        //Vector3.Normalize
        Vector3 centerDirection = Vector3.Normalize(center.position - lantern.transform.position);

        while(lantern.transform.position.y < 15f)
        {
            lantern.transform.position += centerDirection * Time.deltaTime * 0.8f;
            yield return null;
        }

        while(lantern.transform.position.y < 30f)
        {
            lantern.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * 0.5f;
            yield return null;
        }

        if (GameManager.debug)
            Destroy(lantern);
        else
            PhotonNetwork.Destroy(lantern);

        yield break;
    }

    IEnumerator MoveVillager(GameObject villager)
    {
        Debug.Log("move villager: move");
        float range = Random.Range(4,10);
        Vector3 origin = villager.transform.position;
        Vector3 target = villager.transform.position + villager.transform.forward * range;

        while(Vector3.Distance(target, villager.transform.position) > 0.3)
        {
            villager.transform.position = Vector3.Slerp(villager.transform.position, target, Time.deltaTime * 1f);
            yield return null;
        }

        //release kongming
        GameObject lantern;
        if (GameManager.debug)
            lantern = Instantiate(lanternPrefab, target+new Vector3(0,1,0), Quaternion.identity);
        else
            lantern = PhotonNetwork.Instantiate(lanternPrefab.name, target+ new Vector3(0, 1, 0), Quaternion.identity, 0);

        Animator anim = villager.GetComponent<Animator>();
        anim.SetTrigger("Release");

        yield return new WaitForSeconds(2f);
        StartCoroutine(MoveLantern(lantern));

        villager.transform.forward = -villager.transform.forward;
        while (Vector3.Distance(origin, villager.transform.position) > 0.3)
        {
            villager.transform.position = Vector3.Slerp(villager.transform.position, origin, Time.deltaTime * 1f);
            yield return null;
        }

        if (GameManager.debug)
            Destroy(villager);
        else
            PhotonNetwork.Destroy(villager);
        //Destroy(villager);

        yield break;
    }

    IEnumerator Spawn()
    {
        while(true)
        {

            Transform choose = spawnLocations[Random.Range(0, spawnLocations.Length)];

            //choose = new Vector3(0, 0, 30);

            GameObject villager;
            if (GameManager.debug)
                villager = Instantiate(villagerPrefeb, choose.position, choose.rotation);
            else
                villager = PhotonNetwork.Instantiate(villagerPrefeb.name, choose.position, choose.rotation, 0);

            villager.transform.forward = -villager.transform.forward;
            StartCoroutine(MoveVillager(villager));

            yield return new WaitForSeconds(SpawnCD);
        }

        yield break;
    }


}
