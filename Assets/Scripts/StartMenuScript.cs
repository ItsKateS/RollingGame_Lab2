using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenuScript : MonoBehaviour
{
    public GameObject levelSelection;
    public GameObject loadingScreen;
    public GameObject helpInfo;

    public CanvasGroup canvasGroup;
    float fadeDuration = 1f;

    public void MenuStart(Button Start)
    {
        helpInfo.SetActive(false);
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);

        if (levelSelection.activeSelf == true)
            levelSelection.SetActive(false);
        else
            levelSelection.SetActive(true);
    }

    public void Help()
    {
        helpInfo.SetActive(true);
        StartCoroutine(FadeIn(0f, 1f));
        levelSelection.SetActive(false);
    }

    public void MenuExit()
    {
        Application.Quit();
    }

    private IEnumerator FadeIn(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }

    public void LevelSelect(int level)
    {
        loadingScreen.SetActive(true);
        Time.timeScale = 1f;
        StartCoroutine(LoadLevelAsync(level));
    }

    private IEnumerator LoadLevelAsync(int level)
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);
        while (!asyncLoad.isDone) yield return null;
    }
}
