using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NianAI : MonoBehaviour {

    //moving place limit
    public float limit_x_min = 0f;
    public float limit_x_max = 0f;
    public float limit_z_min = 0f;
    public float limit_z_max = 0f;

    //range
    public Detection danger;
    public Detection attack;

    //coroutine
    Coroutine currentCoroutine = null;
    int current_strategy = 0;
    Coroutine facingCoroutine = null;
    public float facingSpeed = 0f;
    Transform facingTarget;

    //follow
    public float followSpeed = 0f;
    Coroutine followCoroutine;
    public float followStopDistance = 0f;

    //attack
    public float attackCD = 0f;
    Coroutine attackCoroutine;

    //dodge
    public float dodgeSpeed = 0f;
    public Coroutine dodgeCoroutine;
    public float dodgeDelay = 0f;
    bool dodgeLock = false;
    public float dodgeRange = 0f;
    Vector3 dodgingTarget;


    //hang
    public float hangSpeed = 0f;
    Coroutine hangCoroutine;

    //player 2
    public Transform player2;
    public float safeHeight = 0f;
    private bool distance;

    //strategy
    //0:follow
    //1:attack
    //2:dodge
    //3.hang

    void Start()
    {
        currentCoroutine = StartCoroutine(Follow());
        facingTarget = player2;
        facingCoroutine = StartCoroutine(Facing());
    }

    void Update()
    {
        int strategy = Analysis();
        //Debug.Log("strategy = " + strategy);
        if (strategy == current_strategy)
            return;

        if (dodgeLock)
            return;

        current_strategy = strategy;
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        switch (strategy)
        {
            case 0:
                currentCoroutine = StartCoroutine(Follow());
                break;
            case 1:
                currentCoroutine = StartCoroutine(Attack());
                break;
            case 2:
                currentCoroutine = StartCoroutine(Dodge());
                break;
            case 3:
                currentCoroutine = StartCoroutine(Hang());
                break;

            default:
                break;
        }
    }

   

    int Analysis()
    {
        if (danger.isDetected)
            return 2;
        if (attack.isDetected)
            return 1;

        if (player2.position.y > safeHeight)
        {
            return 3; 
        }
        if (player2.position.y <= safeHeight)
        {
            return 0;
        }
        Debug.Log("unexpected strategy");
        return -1;
    }

    IEnumerator Facing()
    {
        while (true)
        {
            if (!dodgeLock)
            { 
                Vector3 targerPosition = new Vector3(facingTarget.position.x, transform.position.y, facingTarget.position.z);
                var targetRotation = Quaternion.LookRotation(targerPosition - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, facingSpeed * Time.deltaTime);
                yield return null;
            }
            else //dodging facing
            {
                Vector3 targerPosition = new Vector3(dodgingTarget.x, transform.position.y, dodgingTarget.z);
                var targetRotation = Quaternion.LookRotation(targerPosition - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, facingSpeed * Time.deltaTime);
            }
           
        }
        yield break;
    }

    bool CheckMoveable()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player2.position.x, player2.position.z));

        if (distance <= followStopDistance)
            return false;

        if (transform.position.x > limit_x_max)
            return false;

        if (transform.position.x < limit_x_min)
            return false;

        if(transform.position.z > limit_z_max)
            return false;

        if (transform.position.z < limit_z_min)
            return false;

        return true;
    }

    IEnumerator Follow()
    {
        while (Analysis() == 0)
        {
            //Debug.Log("Following...");
            if(CheckMoveable())
                transform.Translate(Vector3.forward * followSpeed * Time.deltaTime);
            yield return null;
        }
        yield break;
    }

    IEnumerator Attack()
    {
        while (Analysis() == 1)
        {
            Debug.Log("Attacking...");
            //anim.SetTrigger("attack");
            yield return new WaitForSeconds(attackCD);
        }
        yield break;
    }

    IEnumerator Dodge()
    {
        dodgeLock = true;
        yield return new WaitForSeconds(dodgeDelay);

        float x = Random.Range(transform.position.x - dodgeRange, transform.position.x + dodgeRange);
        float z = Random.Range(transform.position.z - dodgeRange, transform.position.z + dodgeRange);

        x = Mathf.Clamp(x, limit_x_min, limit_x_max);
        z = Mathf.Clamp(z, limit_z_min, limit_z_max);

        dodgingTarget = new Vector3(x, 0, z);
        
        while(Vector3.Distance(dodgingTarget,transform.position) > 0.1f) // to be public
        {
            transform.position = Vector3.Slerp(transform.position, dodgingTarget, Time.deltaTime * dodgeSpeed);
        }

        dodgeLock = false;
        yield break;
    }

    IEnumerator Hang() //how about attack player 1 ?
    {
        while(Analysis() == 3)
        {
            Debug.Log("Hanging...");
            yield return new WaitForSeconds(1f);
        }
        yield break;
    }


}
