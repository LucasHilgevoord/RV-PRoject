using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private int totalTimeSec;
    [SerializeField]
    private Text timerObj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timerObj.text = Mathf.Floor(totalTimeSec / 60).ToString("00") + ":" + Mathf.FloorToInt(totalTimeSec % 60).ToString("00");
    }
}
