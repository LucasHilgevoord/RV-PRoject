using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameCam : MonoBehaviour {

    GameObject[] ball;
    [SerializeField]
    Transform resetFocus;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        ball = GameObject.FindGameObjectsWithTag("Ball");
        if (ball.Length > 0 && (transform.eulerAngles.y < 15 || transform.eulerAngles.y > 345))
        {
            Quaternion targetRotation = Quaternion.LookRotation((resetFocus.position + (ball[ball.Length - 1].transform.position - resetFocus.position) / 1.4f) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
        } else
        {
            Quaternion targetRotation = Quaternion.LookRotation(resetFocus.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
        }
    }
}

