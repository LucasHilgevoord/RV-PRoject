using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

	public void LoginMenu(){
        Application.LoadLevel(0);
    }
    public void MainMenu()
    {
        Application.LoadLevel(1);
    }
    public void MainGame()
    {
        Application.LoadLevel(2);
    }
    public void RewardMenu()
    {
        Application.LoadLevel(3);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
