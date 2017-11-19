using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : MonoBehaviour {

    public Animator anim;

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
    public GameObject lantern;
    public GameObject people;

    float t = 0.0f;
    public float minimum = -1.0f;
    public float maximum = 1.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {




        if(t < 1.0f)
        {
            this.transform.position = Vector3.Lerp(inside.position, outside.position, t);
            t += speedVillager * Time.deltaTime * Random.Range(0.2f, 3.0f);
            
        }

        else
        {
            if (!releaseLantern)
            {
                StartCoroutine(ReleaseLight());
                releaseLantern = true;
            }

        }
        




    }


    IEnumerator ReleaseLight()
    {
        if (!villagerGone)
        {

            anim.SetTrigger("Release");
            yield return new WaitForSeconds(2.0f);

        }

        if (t > 1.0f && t < 2.0f)
        {
            
            lantern.transform.position = Vector3.Lerp(lanternPosition.position, inTheMiddle.position, t - 1);
            people.transform.position = Vector3.Lerp(peoplePosition.position, inside.position, t - 1);
            people.transform.LookAt(outside);
            t += speedVillager * Time.deltaTime * Random.Range(0.2f, 3.0f);

        }


        else if (t > 2.0f && t < 3.0f)
        {
            villagerGone = true;
            Destroy(people, 0.1f);
            lantern.transform.position += new Vector3(0.0f, 0.4f * speedLanternUp * Random.Range(0.2f, 3.0f), 0.0f);
        }

    }
}
