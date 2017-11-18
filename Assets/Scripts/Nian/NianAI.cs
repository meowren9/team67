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

    //strategy
    int current_strategy = 0;
    public int status;

    //facing
    Coroutine facingCoroutine = null;
    public float facingSpeed = 0f;
    Transform facingTarget;

    //follow
    public float followSpeed = 0f;
    public float followStopDistance = 0f;

    //attack
    public float attackCD = 0f;

    //dodge
    public float dodgeSpeed = 0f;
    public Coroutine dodgeCoroutine;
    public float dodgeDelay = 0f;
    bool dodgeLock = false;
    public float dodgeRange = 0f;
    Vector3 dodgingTarget;


    //hang
    public float hangSpeed = 0f;
    Vector3 hangingTarget;

    //die
    public Transform[] turnTarget;
    public float FlyingSpeed = 0f;


    //player 2
    public Transform player2;
    public float safeHeight = 0f;


    //health
    public NianHealth health;
    bool dead = false;

    

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
        hangingTarget = new Vector3(0, transform.position.y, 0);
    }

    void Update()
    {
        if(health.status == 2)
        {
            if(!dead)
            {
                if(currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                    currentCoroutine = null;
                }
                currentCoroutine = StartCoroutine(Die());
                dead = true;
            }
            return;
        }

        if (dodgeLock)
            return;

        int strategy = Analysis();
        status = strategy;
        if (strategy == current_strategy)
            return;

        current_strategy = strategy;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
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
        while (!dead)
        {
            Vector3 targerPosition = Vector3.forward;
            if (current_strategy == 3)
            {
               targerPosition = new Vector3(hangingTarget.x, transform.position.y, hangingTarget.z);
            } else if(current_strategy == 2)
            {
                targerPosition = new Vector3(dodgingTarget.x, transform.position.y, dodgingTarget.z);
            } else
            {
                targerPosition = new Vector3(facingTarget.position.x, transform.position.y, facingTarget.position.z);
            }

            var targetRotation = Quaternion.LookRotation(targerPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, facingSpeed * Time.deltaTime);
            yield return null;

        }
        yield break;
    }

    bool CheckMoveable()
    {
        float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player2.position.x, player2.position.z));

        if (distance <= followStopDistance)
            return false;

        if (transform.position.x > limit_x_max)
        {
            transform.position = new Vector3(limit_x_max, transform.position.y, transform.position.z);
            return false;
        }
           

        if (transform.position.x < limit_x_min)
        {
            transform.position = new Vector3(limit_x_min, transform.position.y, transform.position.z);
            return false;
        }

        if (transform.position.z > limit_z_max)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, limit_z_max);
            return false;
        }

        if (transform.position.z < limit_z_min)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, limit_z_min);
            return false;
        }

        return true;
    }

    IEnumerator Follow()
    {
        while (Analysis() == 0)
        {
            Debug.Log("Following...");
            if(CheckMoveable())
            {
                transform.Translate(Vector3.forward * followSpeed * Time.deltaTime);
            }
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
        Debug.Log("dodging...");
        
        yield return new WaitForSeconds(dodgeDelay);

        float x = Random.Range(transform.position.x - dodgeRange, transform.position.x + dodgeRange);
        float z = Random.Range(transform.position.z - dodgeRange, transform.position.z + dodgeRange);

        x = Mathf.Clamp(x, limit_x_min, limit_x_max);
        z = Mathf.Clamp(z, limit_z_min, limit_z_max);

        dodgingTarget = new Vector3(x, transform.position.y, z);

        dodgeLock = true;

        while (Vector3.Distance(dodgingTarget, transform.position) > 0.3f) // to be public
        {
            transform.position = Vector3.Lerp(transform.position, dodgingTarget, Time.deltaTime * dodgeSpeed);
            yield return null;
        }

        dodgeLock = false;
        yield break;
    }

    IEnumerator Hang() //how about attack player 1 ?
    {
        while(Analysis() == 3)
        {
            Debug.Log("Hanging...");
            float x = Random.Range(limit_x_min, limit_x_max);
            float z = Random.Range(limit_z_min, limit_z_max);

            hangingTarget = new Vector3(x, transform.position.y, z);

            while (Vector3.Distance(hangingTarget, transform.position) > 0.3f) // to be public
            {
                transform.position = Vector3.Lerp(transform.position, hangingTarget, Time.deltaTime * hangSpeed);
                yield return null;
            }

        }
        yield break;
    }

    //rotate?

    IEnumerator Die()
    {

        //anything else?

        int turn_len = turnTarget.Length;
        //todo: facing?
        for(int i = 0; i < turn_len; i++)
        {
            while (Vector3.Distance(turnTarget[i].position, transform.position) > 0.3f) // to be public
            {
                transform.position = Vector3.Lerp(transform.position, turnTarget[i].position, Time.deltaTime * FlyingSpeed);
                yield return null;
            }
            //todo: turn delay
        }
        
        //todo: explode

        yield break;
    }


    }
