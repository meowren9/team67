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
            print("Hurt");
            ShakeCamera();
            StartCoroutine(DelayShakeEnd());
        }

    }


    public void ShakeCamera()
    {
        transform.localPosition -= deltaPosition;
        deltaPosition = Random.insideUnitCircle / 3.0f;
        transform.position += deltaPosition;
        //this.transform.localPosition = new Vector3(0, 0, 0);
    }


    IEnumerator DelayShakeEnd()
    {
        yield return new WaitForSeconds(0.3f);
        Database.hurt = false;
        
    }
}
