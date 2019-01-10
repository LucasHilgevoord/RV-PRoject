using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcRotation : MonoBehaviour {

    [SerializeField]
    private GameObject npcHead;
    [SerializeField]
    private GameObject npcBody;

    private GameObject ball;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ball = GameObject.FindGameObjectWithTag("Ball");
        BodyRotation();
        HeadRotation();
    }

    //can be done easier! Will fix this later.
    void BodyRotation() {
        var lookPos = ball.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        npcBody.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }

    void HeadRotation()
    {
        var lookPos = ball.transform.position - transform.position;
        lookPos.z = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        npcHead.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }
}
