using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauzeMenu : MonoBehaviour {

    [SerializeField]
    GameObject pauzeMenu;
    public bool pauzeOpen = false;

    //NOTE: Start dispenser through another way later!
    RobotBehavior robotStart;
    [SerializeField]
    private GameObject robot;

    void Start () {
        robotStart = robot.GetComponent<RobotBehavior>();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauzeOpen)
            {
                pauzeOpen = !pauzeOpen;
                pauzeMenu.SetActive(true);
            } else
            {
                pauzeOpen = !pauzeOpen;
                pauzeMenu.SetActive(false);
                robotStart.StartCoroutine("RotateLerp");
            }
        }
    }
}
