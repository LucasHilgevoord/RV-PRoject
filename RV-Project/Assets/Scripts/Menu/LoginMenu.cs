using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour {

    public static string studentID;
    [SerializeField]
    private Text textField;
    [SerializeField]
    private GameObject notValidObj;

    FadeManager fadeIn;

    private void Start()
    {
        fadeIn = GetComponent<FadeManager>();
        fadeIn.StartCoroutine("CloseWindow");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Enter");
            ButtonClicked();
            
        }
    }

    public void ButtonClicked()
    {
        if (textField.text.Length == 5)
        {
            studentID = textField.text;
            fadeIn.StartCoroutine("OpenWindow");
        }
        else
        {
            textField.text = null;
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
