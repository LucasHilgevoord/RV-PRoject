using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauzeMenu : MonoBehaviour {

    [SerializeField]
    GameObject pauzeMenu;
    public bool pauzeOpen = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauzeOpen)
            {
                pauzeOpen = !pauzeOpen;
                pauzeMenu.SetActive(true);
            } else
            {
                pauzeMenu.SetActive(false);
            }
        }
    }
}
