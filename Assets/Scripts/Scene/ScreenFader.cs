using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup _fadeCanvasGroup;
    [SerializeField] private float _fadeDuration = 0.5f;


    public void TransitionToScene(string sceneName) =>
        StartCoroutine(Transition(sceneName));

    private IEnumerator Transition(string sceneName)
    {
        yield return StartCoroutine(FadeOut());
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float timer = 0;

        while (timer < _fadeDuration)
        {
            _fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / _fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        _fadeCanvasGroup.alpha = 0f;
    }

    private IEnumerator FadeOut()
    {
        float timer = 0;

        while (timer < _fadeDuration)
        {
            _fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / _fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        _fadeCanvasGroup.alpha = 1f;
    }
}
