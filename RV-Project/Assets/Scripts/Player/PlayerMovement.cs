using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    GameObject robot;
    RobotBehavior ballPos;
    [SerializeField]
    private float movementSpeed = 1f;


	// Use this for initialization
	void Start () {
        ballPos = robot.GetComponent<RobotBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
        //Quaternion targetRotation = Quaternion.LookRotation(ballPos.newBallPos - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
        if (Vector3.Distance(transform.position, ballPos.newBallPos) > 1f && ballPos.newBallPos != new Vector3(0,0,0))
        {
            transform.position = Vector3.Lerp(transform.position, ballPos.newBallPos, movementSpeed * Time.deltaTime);
        }
    }

    public void movePlayer(float movingSpeed)
    {
        transform.position += Vector3.right * movingSpeed * Time.deltaTime;
    }
}
