using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour {

    public static int points;
    [SerializeField]
    private Text ScoreText;
    string fmt = "00000";

	// Update is called once per frame
	void Update () {
        ScoreText.text = points.ToString(fmt);
	}

    
}
