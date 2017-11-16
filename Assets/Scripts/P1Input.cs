using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Input : MonoBehaviour
{


    bool EnterArea;
    bool pressTrigger;
    bool spaceDown;
    public GameObject tutorial;
    public Texture[] textureDrag, textureRelease, texturePull;

    bool isDragTutorial = true;
    bool isPullTutorial = false;
    bool isReleaseTutorial = false;
    bool isWaitingRelease = false;
    bool isWaitingPull = false;
    bool isWaitingDestroy = false;


    int tutorialWatingTime = 8;

    int count = 0;
    int count1 = 0;
    void Update()
    {

        //tutorial part

        if((Database.dragBack == true || Database.dragForward == true || 
           Database.dragLeft == true || Database.dragRight == true )&& isDragTutorial)
        {

            isDragTutorial = false;
            isWaitingRelease = true;


        }

        if (Database.releaseKite  == true && isReleaseTutorial)
        {
            

            isReleaseTutorial = false;
            isWaitingPull = true;
        }

        if (Database.isPull == true && isPullTutorial)
        {

            Destroy(tutorial, 3f);
            Database.isTutorial = false;//game start;
        }







        //tutorial end

        ////if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0)
        if (Input.GetKey(KeyCode.Space))
        {

            spaceDown = true;
        }

        ////if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 0)
        if (Input.GetKeyUp(KeyCode.Space))
        {

            spaceDown = false;
        }

        if (!GameManager.debug)
        {
            if (!PhotonNetwork.isMasterClient) //p1
            {
                return;
            }
        }


        if (Database.moveUp)
        {
            this.transform.position += new Vector3(-0.1f, 0.0f, 0.0f);
        }

        if (Database.moveDown)
        {
            this.transform.position += new Vector3(0.1f, -0.0f, 0.0f);
        }

        if (EnterArea && spaceDown)
        {
            Database.handDrag = true;
        }



        if (!spaceDown)
        {

            Database.handDrag = false;

        }



        ////if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > 0)

        if (Input.GetKeyDown(KeyCode.M))
        {
            OVRInput.SetControllerVibration(1, 20, OVRInput.Controller.LTouch);
            Database.releaseKite = true;
            
        }

        ////if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y == 0)

        if (Input.GetKeyUp(KeyCode.M))
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
            Database.releaseKite = false;
        }




    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "rayArea")
        {
            EnterArea = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "rayArea")
        {
            EnterArea = false;

        }

    }


    void changeTexture()
    {


    }


    void Start()
    {
        StartCoroutine(tutorialPPT());
    }
    IEnumerator tutorialPPT()
    {
        while (true)
        {
            if (isWaitingRelease)
            {
                if (count < tutorialWatingTime)
                {
                    if (tutorial.GetComponent<Renderer>().material.mainTexture == textureDrag[1])
                        tutorial.GetComponent<Renderer>().material.mainTexture = textureDrag[0];

                    else if (tutorial.GetComponent<Renderer>().material.mainTexture == textureDrag[0])
                        tutorial.GetComponent<Renderer>().material.mainTexture = textureDrag[1];
                    count++;

                }
                else
                {
                    isWaitingRelease = false;
                    isReleaseTutorial = true;
                    tutorial.GetComponent<Renderer>().material.mainTexture = textureRelease[1];
                }
                    

                }

            if (isWaitingPull)
            {
                if (count1 < tutorialWatingTime)
                {
                    if (tutorial.GetComponent<Renderer>().material.mainTexture == textureRelease[1])
                        tutorial.GetComponent<Renderer>().material.mainTexture = textureRelease[0];

                    else if (tutorial.GetComponent<Renderer>().material.mainTexture == textureRelease[0])
                        tutorial.GetComponent<Renderer>().material.mainTexture = textureRelease[1];
                    count1++;

                }
                else
                {
                    isWaitingPull = false;
                    isPullTutorial = true;
                    tutorial.GetComponent<Renderer>().material.mainTexture = texturePull[0];
                }


            }

            if (isDragTutorial)
            {
                if (tutorial.GetComponent<Renderer>().material.mainTexture == textureDrag[1])
                    tutorial.GetComponent<Renderer>().material.mainTexture = textureDrag[0];

                else if (tutorial.GetComponent<Renderer>().material.mainTexture == textureDrag[0])
                    tutorial.GetComponent<Renderer>().material.mainTexture = textureDrag[1];

            }

            if (isReleaseTutorial)
            {
                

                    if (tutorial.GetComponent<Renderer>().material.mainTexture == textureRelease[1])
                        tutorial.GetComponent<Renderer>().material.mainTexture = textureRelease[0];

                    else if (tutorial.GetComponent<Renderer>().material.mainTexture == textureRelease[0])
                        tutorial.GetComponent<Renderer>().material.mainTexture = textureRelease[1];

                

            }


            if (isPullTutorial)
            {
                if (tutorial.GetComponent<Renderer>().material.mainTexture == texturePull[0])
                    tutorial.GetComponent<Renderer>().material.mainTexture = texturePull[1];

                else if (tutorial.GetComponent<Renderer>().material.mainTexture == texturePull[1])
                    tutorial.GetComponent<Renderer>().material.mainTexture = texturePull[2];

                else if (tutorial.GetComponent<Renderer>().material.mainTexture == texturePull[2])
                    tutorial.GetComponent<Renderer>().material.mainTexture = texturePull[0];

            }

            yield return new WaitForSeconds(0.4f);
        }
    }



}
