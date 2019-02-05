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
    [SerializeField]
    GameObject gameManager;
    PauzeMenu pauzeScript;

    AudioSource audiosrc;
    [SerializeField]
    AudioClip shotSound;

    [SerializeField]
    float rotateDuration = 2f;

    public Vector3 newBallPos;

    // Use this for initialization
    void Start () {
        audiosrc = GetComponent<AudioSource>();
        pauzeScript = gameManager.GetComponent<PauzeMenu>();
    }

    public IEnumerator RotateLerp()
    {
        Vector3 newRotation = arm.transform.eulerAngles;
        //newRotation.y = Random.Range(-80f, -100f);
        newRotation.y = Random.Range(75f, 105f);

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
        if (!pauzeScript.pauzeOpen)
        {
            GameObject ballInstance = Instantiate(ball, firePoint.position, firePoint.rotation) as GameObject;
            Rigidbody ballRb = ballInstance.GetComponent<Rigidbody>();
            float velocity = Random.Range(5.7f, 6f);
            ballRb.AddForce(firePoint.forward * velocity * 100);

            Vector3 expectedBallPosition = firePoint.position;
            Vector3 tempVel = firePoint.forward * velocity * 100 * Time.fixedDeltaTime;
            
            while(expectedBallPosition.y > 0)
            {
                tempVel += Physics.gravity * Time.fixedDeltaTime;
                expectedBallPosition += tempVel * Time.fixedDeltaTime;
            }

            newBallPos = expectedBallPosition;
#if UNITY_EDITOR
            debugBallPosition = expectedBallPosition;
#endif

            audiosrc.clip = shotSound;
            audiosrc.Play();
            StartCoroutine("RotateLerp");
            Destroy(ballInstance, 5f);
        }
    }

#if UNITY_EDITOR

    Vector3 debugBallPosition;
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(debugBallPosition, 0.3f);
    }
#endif
}
