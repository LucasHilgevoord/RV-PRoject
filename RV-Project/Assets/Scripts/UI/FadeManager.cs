using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {

    [SerializeField]
    private CanvasGroup canvasGroup = null;
    [SerializeField]
    private float fadeSpeed = 2f;
    [SerializeField]
    private GameObject blackScreen;
    public int sceneNumber;

    public IEnumerator OpenWindow() {
        blackScreen.SetActive(true);
        while (canvasGroup.alpha < 1) {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
            if (canvasGroup.alpha >= 1) {
                Application.LoadLevel(sceneNumber);
            }
        }
    }
    public IEnumerator CloseWindow() {
        blackScreen.SetActive(true);
        canvasGroup.alpha = 1;
        while (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= Time.deltaTime * (fadeSpeed);
            yield return null;
            if (canvasGroup.alpha == 0) {
                blackScreen.SetActive(false);
            }
        }
    }
}
