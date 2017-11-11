using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour {



	void Update () {
        if (Database.moveUp)
        {
            this.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
        }

        if (Database.moveDown)
        {
            this.transform.position += new Vector3(0.0f, -0.1f, 0.0f);
        }
    }
}
