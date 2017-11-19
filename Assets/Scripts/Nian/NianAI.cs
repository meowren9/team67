using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NianAI : Photon.PunBehaviour, IPunObservable
{

    //moving place limit
    public float limit_x_min = 0f;
    public float limit_x_max = 0f;
    public float limit_z_min = 0f;
    public float limit_z_max = 0f;
    public float stable_z_max = 0f;

    public Transform location;

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
    public float fireLastTime;
    public bool IsFiring = false;
    public float fireCD = 0f;
    public ParticleSystem Fire;
    //public CapsuleCollider attackRange;
    public GameObject attackRange;

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
    public bool dead = false;

    //anim
    public Animator nian_anim;

    //strategy
    //0:follow
    //1:attack
    //2:dodge
    //3.hang

    void Awake()
    {
       
        if ((!PhotonNetwork.isMasterClient || GameManager.debug)) //p2
        {
            player2 = GameObject.Find("P2").transform;
            location = GameObject.Find("location").transform;

            facingTarget = player2;
            hangingTarget = new Vector3(0, transform.position.y, 0);//init
            //currentCoroutine = StartCoroutine(Follow());
            facingCoroutine = StartCoroutine(Facing());
        }
           
        //both
        StartCoroutine(FireSync());
    }



    void Update()
    {
        //to be master control

        if (!(!PhotonNetwork.isMasterClient || GameManager.debug))
            return;

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

        //if (dodgeLock)
        //    return;

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

    IEnumerator UpdateZmax() //master
    {
        while(!dead)
        {
            limit_z_max = Mathf.Max(player2.position.z - 5, stable_z_max);
            yield return null;
        }
        yield break;
    }

    IEnumerator FireSync()
    {
        Debug.Log("in fire sync");
        while(!dead)
        {
            var emission = Fire.emission;
            emission.enabled = IsFiring;
            yield return null;
        }
        yield break;
    }

    IEnumerator Facing()
    {
        while (!dead)
        {

            Vector3 targerPosition = Vector3.forward;
            if (current_strategy == 3)
            {
                //targerPosition = new Vector3(hangingTarget.x, transform.position.y, hangingTarget.z);
                targerPosition = new Vector3(facingTarget.position.x, transform.position.y, facingTarget.position.z);
            } else if(current_strategy == 2)
            {
                //targerPosition = new Vector3(dodgingTarget.x, transform.position.y, dodgingTarget.z);
                targerPosition = new Vector3(facingTarget.position.x, transform.position.y, facingTarget.position.z);
            } else
            {
                //targerPosition = new Vector3(facingTarget.position.x, transform.position.y, facingTarget.position.z);
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
        //while (Analysis() == 0)
        //{
        //    Debug.Log("Following...");
        //    if (CheckMoveable())
        //    {
        //        transform.Translate(Vector3.forward * followSpeed * Time.deltaTime);
        //    }
        //    else
        //    {

        //    }
        //    yield return null;
        //}

        while(!dead)
        {
            var targetPosition = new Vector3(location.position.x, transform.position.y, location.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
            if(Vector3.Distance(targetPosition,transform.position) < 0.3)
            {
                attackRange.SetActive(true);
                //yield break;
            }
            else
            {
                attackRange.SetActive(false);
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
            if(health.status == 0) //normal attack
            {
                nian_anim.SetTrigger("scratch");
                yield return new WaitForSeconds(attackCD);
            }
            else if(health.status == 1) //fire attack
            {

                if (PhotonNetwork.isMasterClient || GameManager.debug)
                {
                    //Debug.Log("Fire start");
                    IsFiring = true;
                    yield return new WaitForSeconds(fireLastTime);
                    IsFiring = false;
                    yield return new WaitForSeconds(attackCD);
                }
                
            }
            else
            {
                yield break;
            }
        }
        yield break;
    }

    public float smoothTime = 0.3F;
    public float dodgingDistance = 8f;

    IEnumerator Dodge()
    {
        Debug.Log("dodging...");
        
        yield return new WaitForSeconds(dodgeDelay);

        float x = Random.Range(transform.position.x - dodgeRange, transform.position.x + dodgeRange);
        float z = Random.Range(transform.position.z - dodgeRange, transform.position.z + dodgeRange);

        x = Mathf.Clamp(x, limit_x_min, limit_x_max);
        z = Mathf.Clamp(z, limit_z_min, limit_z_max);

        
        //Vector3 TransformPoint = Vector3.zero;
        Vector3 velocity = Vector3.zero;

        //dodgingTarget = new Vector3(x, transform.position.y, z);
        int directionChoose = Random.Range(0, 2);
        Vector3 direction;
        switch (directionChoose)
        {
            case 0:
                direction = transform.forward;
                break;
            case 1:
                direction = transform.right;
                break;
            case 2:
                direction = -transform.right;
                break;
            default:
                direction = transform.forward;
                break;
        }

        dodgingTarget = transform.position - new Vector3(direction.x * Random.Range(1, dodgingDistance), 0, direction.z * Random.Range(1, dodgingDistance));
        
        //dodgeLock = true;

        while (Vector3.Distance(dodgingTarget, transform.position) > 0.3f) // to be public
        {
            transform.position = Vector3.SmoothDamp(transform.position, dodgingTarget, ref velocity, smoothTime);
            //transform.position = Vector3.Lerp(transform.position, dodgingTarget, Time.deltaTime * dodgeSpeed);
            yield return null;
        }

        //dodgeLock = false;
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

            //roar

        }
        yield break;
    }

    //rotate?

    IEnumerator Die()
    {
        GameManager.nian_defeat = true;
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


    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(IsFiring);
            //stream.SendNext(active_target_idx);
            //stream.SendNext(alive);
        }
        else
        {
            // Network player, receive data
            this.IsFiring = (bool)stream.ReceiveNext();
            //this.active_target_idx = (int)stream.ReceiveNext();
            //this.alive = (bool)stream.ReceiveNext();
        }
    }
}
