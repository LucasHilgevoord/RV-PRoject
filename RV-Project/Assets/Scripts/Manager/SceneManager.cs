using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    FadeManager fadeIn;

    private void Start()
    {
        fadeIn = GetComponent<FadeManager>();
        
    }

    public void LoginMenu(){
        fadeIn.StartCoroutine("OpenWindow");
        fadeIn.sceneNumber = 0;
    }
    public void MainMenu()
    {
        fadeIn.StartCoroutine("OpenWindow");
        fadeIn.sceneNumber = 1;
        Timer.countUp = 0;
        Points.points = 0;
    }
    public void MainGame()
    {
        fadeIn.StartCoroutine("OpenWindow");
        fadeIn.sceneNumber = 2;
    }
    public void RewardMenu()
    {
        fadeIn.StartCoroutine("OpenWindow");
        fadeIn.sceneNumber = 3;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
