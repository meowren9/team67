using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{


    public static Database _instance;

    public static bool moveForward = false;
    public static int direction = 1;
    public static bool startFollow = false;
    public static bool facingLeft = false;
    public static bool facingRight = false;

    public static bool moveUp = false;
    public static bool moveDown = false;


    public static bool handDrag = false;
    public static bool spaceDown = false;
    public static bool dragLeft = false;
    public static bool dragRight = false;
    public static bool dragBack = false;
    public static bool dragForward = false;


    public static bool dragCenter = false;


    public static bool releaseKite = false;



    public static bool isPull = false;

    public static bool win = false;

    public static bool isTutorial = true;
    public static bool finishFight = false;

}