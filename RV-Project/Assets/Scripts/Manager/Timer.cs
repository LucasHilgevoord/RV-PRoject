using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public static float countUp;
    [SerializeField]
    private Text timerObj;

	void Update () {
        countUp += Time.deltaTime;
        timerObj.text = (((Mathf.Floor(countUp / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(countUp % 60f).ToString("00"));
    }
}
