using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("right"))
        {
            movePlayer(3.0f);
        }
        if (Input.GetKey("left"))
        {
            movePlayer(-3.0f);
        }
    }

    public void movePlayer(float movingSpeed)
    {
        transform.position += Vector3.right * movingSpeed * Time.deltaTime;
    }
}
