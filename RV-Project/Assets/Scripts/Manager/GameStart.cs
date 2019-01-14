using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup = null;
    [SerializeField]
    private GameObject startText;
    [SerializeField]
    private GameObject robot;

    FadeManager fadeIn;
    RobotBehavior robotStart;
    
    private Timer startTimer;
    private float fadeSpeed = 2f;
    private AudioSource audiosrc;

    void Start()
    {
        fadeIn = GetComponent<FadeManager>();
        fadeIn.StartCoroutine("CloseWindow");
        robotStart = robot.GetComponent<RobotBehavior>();
        audiosrc = GetComponent<AudioSource>();

        startTimer = GetComponent<Timer>();
        canvasGroup.alpha = 0;
        startText.SetActive(false);

        StartGame();
    }

    void StartGame()
    {
        StartCoroutine(OpenWindow());
        StartCoroutine(CloseWindow());
    }

    IEnumerator OpenWindow()
    {
        yield return new WaitForSeconds(.5f);
        startTimer.gameStart = true;
        startText.SetActive(true);
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

    IEnumerator CloseWindow()
    {
        yield return new WaitForSeconds(2.5f);
        while (canvasGroup.alpha > 0 && startText != null)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        if (canvasGroup.alpha == 0)
        {
            startText.SetActive(false);
            audiosrc.Play();
            robotStart.StartCoroutine("RotateLerp");
        }
    }
}
