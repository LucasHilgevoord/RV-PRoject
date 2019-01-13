using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehavior : MonoBehaviour {

    [SerializeField]
    GameObject ball;
    [SerializeField]
    Transform firePoint;
    [SerializeField]
    Transform arm;

    AudioSource audiosrc;
    [SerializeField]
    AudioClip shotSound;

    [SerializeField]
    float rotateDuration = 2f;
    [SerializeField]
    float shootSpeed;
    bool rotating = false;

    // Use this for initialization
    void Start () {
        audiosrc = GetComponent<AudioSource>();
        StartCoroutine("RotateLerp");
        //Shoot();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("up"))
        {
            StartCoroutine("RotateLerp");
            //Shoot();
        }
    }

    IEnumerator RotateLerp()
    {
        Vector3 newRotation = arm.transform.eulerAngles;
        newRotation.y = Random.Range(-80f, -100f);

        float counter = 0;
        while (counter < rotateDuration)
        {
            counter += Time.deltaTime;
            arm.transform.rotation = Quaternion.Lerp(arm.transform.rotation, Quaternion.Euler(newRotation), counter / rotateDuration);
            yield return null;
        }

        Shoot();
        yield return 0;
    }

    void Shoot()
    {
        GameObject ballInstance = Instantiate(ball, firePoint.position, firePoint.rotation) as GameObject;
        Rigidbody ballRb = ballInstance.GetComponent<Rigidbody>();
        //ballRb.velocity = velocity * firePoint.forward;
        float velocity = Random.Range(5.7f, 6.5f);
        ballRb.AddForce(firePoint.forward * velocity * 100);

        audiosrc.clip = shotSound;
        audiosrc.Play();
        StartCoroutine("RotateLerp");
        Destroy(ballInstance, 5f);
        
    }
}
