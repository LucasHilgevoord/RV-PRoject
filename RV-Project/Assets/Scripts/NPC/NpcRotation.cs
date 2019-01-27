using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcRotation : MonoBehaviour {

    [SerializeField]
    private GameObject npcHead;
    [SerializeField]
    private GameObject npcBody;

    private GameObject[] ball;
    private Rigidbody rb;

    [SerializeField]
    private Material[] randomMat;
    [SerializeField]
    private GameObject bodyMatObj;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        Invoke("Jump", Random.Range(0.5f,5f));
        npcBody.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        npcHead.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        bodyMatObj.GetComponent<Renderer>().material = randomMat[Random.Range(0, randomMat.Length)];
    }
	
	// Update is called once per frame
	void Update () {
        ball = GameObject.FindGameObjectsWithTag("Ball");
        if (ball.Length > 0)
        {
            BodyRotation();
            HeadRotation();
            //Vector3 forward = npcBody.transform.TransformDirection(Vector3.forward) * 100;
            //Debug.DrawRay(npcBody.transform.position, forward, Color.green);
        }
    }

    //can be done easier! Will fix this later.
    void BodyRotation() {
        Quaternion targetRotation = Quaternion.LookRotation(ball[ball.Length - 1].transform.position - npcBody.transform.position);
        targetRotation.z = 0; targetRotation.x = 0;
        npcBody.transform.rotation = Quaternion.Slerp(npcBody.transform.rotation, targetRotation, .7f * Time.deltaTime);
    }

    void HeadRotation()
    {
        Quaternion targetRotation = Quaternion.LookRotation(ball[ball.Length - 1].transform.position - npcHead.transform.position);
        npcHead.transform.rotation = Quaternion.Slerp(npcHead.transform.rotation, targetRotation, 1 * Time.deltaTime);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * 100f);
        Invoke("Jump", Random.Range(0.5f, 5f));
    }
}
