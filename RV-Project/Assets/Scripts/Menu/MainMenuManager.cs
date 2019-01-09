using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    [SerializeField]
    Text idText;

	// Use this for initialization
	void Start () {
        idText.text = LoginMenu.studentID;
        if (idText.text.Length == 0)
        {
            idText.text = "00000";
        }
	}
}
