using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardMenuManger : MonoBehaviour {

    FadeManager fadeIn;
    [SerializeField]
    Text timer;
    [SerializeField]
    Text balance;
    [SerializeField]
    Text points;
    string fmt = "0000";
    [SerializeField]
    float multiplier = 0.00001f;

    // Use this for initialization
    void Start () {
        fadeIn = GetComponent<FadeManager>();
        fadeIn.StartCoroutine("CloseWindow");
        timer.text = (((Mathf.Floor(Timer.countUp / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(Timer.countUp % 60f).ToString("00"));
        points.text = Points.points.ToString(fmt);
        //Balance = tijd * (1 + C*punten)
        CalculateBalance();
    }

    void CalculateBalance()
    {
        float calBallance = Timer.countUp * (1 + multiplier * Points.points) / 1000;
        balance.text = calBallance.ToString("0.00");

    }
}
