using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMenuOpen : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    float duration = 1f;

    public void ShowMenuPanel(GameObject MenuPanel)
    {
        MenuPanel.SetActive(true);
        StartCoroutine(FadeIn());
        Time.timeScale = 0f;
    }

    public void Resume(GameObject MenuPanel)
    {
        MenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MainMenu(GameObject LoadingScreen)
    {
        LoadingScreen.SetActive(true);
        Time.timeScale = 1f;
        StartCoroutine(LoadLevelAsync());
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
            yield return null; 
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator LoadLevelAsync()
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
        while (!asyncLoad.isDone) yield return null; 
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
