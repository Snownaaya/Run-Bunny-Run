using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Assets.Scripts.Audio;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private Button _transitionScene;

    private Sequence _animation;

    public void OnTransitionToScene(string sceneName)
    {
        StartCoroutine(TransitionCoroutine(sceneName));
        BackgroundMusic.Instance.PlayMusic();
    }

    public IEnumerator TransitionCoroutine(string sceneName)
    {
        yield return _canvasGroup.DOFade(0f, _fadeDuration)
                 .SetEase(Ease.Linear)
                 .Play()
                 .SetAutoKill(true)
                 .WaitForCompletion();


        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        yield return operation;
    }
}