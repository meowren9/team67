using UnityEngine;
using System.Collections;
public class PlaneToPoint : MonoBehaviour
{
    public Transform target;
    public Transform playerSky;
    public Transform playerGround;
    


    void Update()
    {

        //Plane 类提供了处理平面所用到的基本算法
        //3的点构成一个面,或者坐标和法线构成一个面
        //这里使用planeTarget　坐标和法线,平面的法线也就是正面方向(up方向)
        //Plane plane = new Plane(planeTarget.up, planeTarget.position);
        Plane plane = new Plane(playerSky.position, playerGround.position, playerSky.position + new Vector3(0.0f, 0.2f, 0.0f));
        float distance = plane.GetDistanceToPoint(target.position);
        //Debug.Log("distance:" + distance);

        if(distance > 0.2f)
        {
            if (Database.handDrag)
            {
                Database.dragRight = true;
                Database.dragLeft = false;
            }
        }

        if(distance < -0.2f)
        {
            if (Database.handDrag)
            {
                Database.dragLeft = true;
                Database.dragRight = false;
            }
        }

        if(distance < 0.19f && distance > -0.19f)
        {
            Database.dragLeft = false;
            Database.dragRight = false;
        }

    }
}