using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

    public GameObject centerBody;
    public GameObject handObj;

    private Vector3 RacketPos;
    private Quaternion RacketRot;

    private float dir = 0;
    private float rot = 0;
    
    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float rotateSpeed = 200f;
	
	// Update is called once per frame
	void Update ()
    {
        RacketPos = transform.position;
        RacketRot = handObj.transform.rotation;

        Movement();

        Debug.Log(RacketRot.z);

        // Border left
        if (RacketPos.x <= centerBody.transform.localPosition.x - 0.5f)
        {
            if (dir == -1f)
            {
                moveSpeed = 0;
            }
            else if (dir == 1f)
            {
                moveSpeed = 5f;
            }
            Debug.Log("Left");
        }
        // Border right
        else if (RacketPos.x >= centerBody.transform.localPosition.x + 0.5f)
        {
            if (dir == 1f)
            {
                moveSpeed = 0;
            }
            else if (dir == -1f)
            {
                moveSpeed = 5f;
            }
            Debug.Log("Right");
        }
        else
        {
            Debug.Log("Middle");
        }
        

        // Border up
        if (RacketRot.z >= centerBody.transform.rotation.z + 0.5f)
        {
            if (rot == 2f)
            {
                rotateSpeed = 0;
            }
            else if (rot == -2f)
            {
                rotateSpeed = 200f;
            }
            Debug.Log("Right");
        }
        // Border down
        else if (RacketRot.z <= centerBody.transform.rotation.z - 0.5f)
        {
            if (rot == -2f)
            {
                rotateSpeed = 0;
            }
            else if (rot == 2f)
            {
                rotateSpeed = 200f;
            }
            Debug.Log("Right");
        }
    }

    void Movement()
    {

        // Movement horizontal
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            dir = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            dir = 1f;
        }

        // Movement vertical
        if (Input.GetKey(KeyCode.UpArrow))
        {
            handObj.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            rot = 2f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            handObj.transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
            rot = -2f;
        }
    }
}
