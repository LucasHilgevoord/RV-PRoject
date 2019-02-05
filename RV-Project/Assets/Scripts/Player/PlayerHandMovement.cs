using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandMovement : MonoBehaviour {

    GameObject hndLeft;
    GameObject hndRight;

    Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        hndLeft = GameObject.Find("HandLeft");
        hndRight = GameObject.Find("HandRight");

        if (this.gameObject.name == "Hand_R")
        {
            Debug.Log("Rechts");
            transform.position = hndRight.transform.position;
            //rb.MovePosition(hndRight.transform.position);
            transform.rotation = hndRight.transform.rotation;
            Quaternion rot = transform.rotation;
            rot.y = 0;
        } else
        {
            Debug.Log("Left");
            transform.position = hndLeft.transform.position;
            //rb.MovePosition(hndLeft.transform.position);
            transform.rotation = hndLeft.transform.rotation;
            Quaternion rot = transform.rotation;
            rot.y = 0;

        }
	}
}
