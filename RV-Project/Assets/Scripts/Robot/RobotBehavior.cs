using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehavior : MonoBehaviour {

    [SerializeField]
    GameObject ball;
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    GameObject body;

    [SerializeField]
    AudioSource audiosrc;
    [SerializeField]
    AudioClip shotSound;

    [SerializeField]
    float rotateSpeed;
    [SerializeField]
    float shootSpeed;
    [SerializeField]
    float velocity;

    Quaternion orignalRot;
    Quaternion newRot;

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
        ballRb.velocity = velocity * firePoint.forward; ;

        audiosrc.clip = shotSound;
        audiosrc.Play();
    }
}
