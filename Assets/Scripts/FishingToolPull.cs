using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingToolPull : MonoBehaviour {

    public GameObject[] checkPoint;
    private bool[] isCheckPointClear = new bool[3] { false, false, false };
    private float waitingTime = 0.5f;


    private bool isPlay = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CheckPoint0")
        {
            Check(0);
        }
        if (other.tag == "CheckPoint1")
        {
            Check(1);
        }
        if (other.tag == "CheckPoint2")
        {
            Check(2);
        }
    }

    private void Check(int point)
    {
        if (!isCheckPointClear[0] && !isCheckPointClear[1] && !isCheckPointClear[2])
        {
            isCheckPointClear[point] = true;
            Database.isPull = true;
            StopAllCoroutines();
            StartCoroutine(CheckSpeed(waitingTime));
            return;
        } else
        {
            if (!isCheckPointClear[point])
            {
                if ( point-1 >= 0)
                {
                    if (isCheckPointClear[point - 1])
                    {
                        isCheckPointClear[point - 1] = false;
                        isCheckPointClear[point] = true;
                        Database.isPull = true;
                        StopAllCoroutines();
                        StartCoroutine(CheckSpeed(waitingTime));
                        return;
                    }
                } else
                {
                    if (isCheckPointClear[2])
                    {
                        isCheckPointClear[2] = false;
                        isCheckPointClear[point] = true;
                        Database.isPull = true;
                        StopAllCoroutines();
                        StartCoroutine(CheckSpeed(waitingTime));
                        return;
                    }
                }
            }
            Database.isPull = false;
        }
    }

    private IEnumerator CheckSpeed(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        for (int i =0; i < 3; i++)
        {
            isCheckPointClear[i] = false;
        }
        Database.isPull = false;
    }


}
