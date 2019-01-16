using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    bool isHit = false;

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Ball")
        {
            if (!isHit)
            {
                Debug.Log("HITBALL");
                col.gameObject.GetComponent<Rigidbody>();
                Rigidbody ballRb = col.gameObject.GetComponent<Rigidbody>();
                //ballRb.velocity = velocity * firePoint.forward;
                float velocity = Random.Range(5.7f, 6.5f);
                ballRb.AddForce(-col.transform.forward * velocity * 100);

                isHit = true;
            }
        }
    }
}
