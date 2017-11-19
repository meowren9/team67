using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingCamera : MonoBehaviour
{
    private Vector3 deltaPosition = Vector3.zero;   

    private void Update()
    {
        ShakeCamera();
    }


    public void ShakeCamera()
    {
        transform.localPosition -= deltaPosition; 
        deltaPosition = Random.insideUnitCircle / 3.0f; 
        transform.position += deltaPosition;      
    }

}
