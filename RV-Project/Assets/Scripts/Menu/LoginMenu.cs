﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour {

    public static string studentID;
    private Text textField;
    [SerializeField]
    private GameObject textObject;
    [SerializeField]
    private GameObject notValidObj;

    private void Start()
    {
        textField = textObject.GetComponent<Text>();
    }

    public void ButtonClicked()
    {
        if (textField.text.Length == 5)
        {
            studentID = textField.text;
            Application.LoadLevel(1);
        }
        else
        {
            notValidObj.SetActive(true);
            StartCoroutine(NotValidDelete());
        }
    }
    
    IEnumerator NotValidDelete()
    {
        yield return new WaitForSeconds(1.0f);
        notValidObj.SetActive(false);
    }
}
