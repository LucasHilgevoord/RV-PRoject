using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private float tempSpeed = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKey("right"))
        {
            movePlayer(3.0f);
        }
        if (Input.GetKey("left"))
        {
            movePlayer(-3.0f);
        }
        */
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * tempSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * tempSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * tempSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * tempSpeed * Time.deltaTime;
        }
    }

    public void movePlayer(float movingSpeed)
    {
        transform.position += Vector3.right * movingSpeed * Time.deltaTime;
    }
}
