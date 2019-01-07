using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehavior : MonoBehaviour {

    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private AudioSource audiosrc;
    [SerializeField]
    private AudioClip shotSound;

    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float shootSpeed;
    [SerializeField]
    private float launchForce;

    // Use this for initialization
    void Start () {
        Shoot();

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void Rotate()
    {

    }

    void Shoot()
    {
        GameObject ballInstance = Instantiate(ball, firePoint.position, firePoint.rotation) as GameObject;
        Rigidbody ballRb = ballInstance.GetComponent<Rigidbody>();
        ballRb.velocity = launchForce * firePoint.forward; ;

        audiosrc.clip = shotSound;
        audiosrc.Play();
    }
}
