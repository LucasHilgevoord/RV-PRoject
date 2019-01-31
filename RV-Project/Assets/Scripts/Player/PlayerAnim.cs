using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = gameObject.GetComponent<Animator>();

        anim.SetBool("Default", true);
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.F))
        {
            anim.SetBool("Default", false);
            anim.SetBool("Left", false);
            anim.SetBool("Right", true);
       
            Debug.Log("Pressed");
        }
		
	}
}
