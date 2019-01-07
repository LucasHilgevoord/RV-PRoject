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
    [SerializeField]
    float angle;
    [SerializeField]
    int resolution = 10;

    float gravity;
    float radianAngle;

    private LineRenderer lr; 

    // Use this for initialization
    void Start () {
        Shoot();
        lr = firePoint.GetComponent<LineRenderer>();
        //https://en.wikipedia.org/wiki/Projectile_motion

        gravity = Mathf.Abs(Physics.gravity.y);
        RenderArc();
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

    //Line Rendering
    void RenderArc()
    {
        lr.SetVertexCount(resolution + 1);
        lr.SetPositions(CalculateArcArray());
    }

    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / gravity;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y);
    }
}
