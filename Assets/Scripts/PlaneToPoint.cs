using UnityEngine;
using System.Collections;
public class PlaneToPoint : MonoBehaviour
{
    public Transform target;
    public Transform playerSky;
    public Transform playerGround;



    void Update()
    {


        Plane planeVir = new Plane(playerSky.position, playerGround.position, playerSky.position + 
            new Vector3(0.0f, 0.2f, 0.0f));
        Plane planeHori = new Plane(playerSky.position, playerGround.position, playerSky.position + 
            new Vector3(0.2f, 0.0f, 0.0f));

        float distanceVir = planeVir.GetDistanceToPoint(target.position);
        float distanceHori = planeHori.GetDistanceToPoint(target.position);
        //Debug.Log("distance:" + distanceHori);




        if (distanceHori < -0.04f)//dragback distance
        {
            if (Database.handDrag)
            {
                Database.dragBack = true;
                Database.dragForward = false;
            }
        }


        if (distanceHori > 0.04f)
        {
            if (Database.handDrag)
            {
                Database.dragBack = false;
                Database.dragForward = true;

            }
        }


        if (distanceVir > 0.1f)
        {
            if (Database.handDrag)
            {
                Database.dragRight = true;
                Database.dragLeft = false;
            }
        }

        if (distanceVir < -0.1f)
        {
            if (Database.handDrag)
            {
                Database.dragLeft = true;
                Database.dragRight = false;
            }
        }

        if (distanceVir < 0.09f && distanceVir > -0.09f)
        {
            Database.dragLeft = false;
            Database.dragRight = false;
        }

        if (distanceHori < 0.03f && distanceHori > -0.03f)
        {
            Database.dragBack = false;
            Database.dragForward = false;
        }

    }
}