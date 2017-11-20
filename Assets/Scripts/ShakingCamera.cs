using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingCamera : MonoBehaviour
{
    Vector3 deltaPosition = Vector3.zero;

    void Update()
    {

        if (Database.hurt)
        {
            ShakeCamera();
            StartCoroutine(DelayShakeEnd());
        }

    }


    public void ShakeCamera()
    {
        transform.localPosition -= deltaPosition;
        deltaPosition = Random.insideUnitCircle / 3.0f;
        transform.position += deltaPosition;
    }


    IEnumerator DelayShakeEnd()
    {
        yield return new WaitForSeconds(2.0f);
        Database.hurt = false;
    }
}
