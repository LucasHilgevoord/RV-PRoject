using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour {

    bool isHit = false;

    private void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag == "Racket")
        {
            if (!isHit)
            {
                Points.points++;
                isHit = true;
            }
        }
    }
}
