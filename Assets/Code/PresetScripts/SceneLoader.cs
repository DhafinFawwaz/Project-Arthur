using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Loading _loadingBar;
    string _sceneToLoad;

    public delegate void OnLoadingEndDelegate();
    OnLoadingEndDelegate OnLoadingEnd;
    public void AddOnLoadingEnd(OnLoadingEndDelegate func) => OnLoadingEnd += func;

    public void LoadScene(string sceneName)
    {
        _loadingBar.gameObject.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        yield return null;
        Time.timeScale = 0;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);
            _loadingBar.Fill(progress);
            yield return null;
        }
        if(OnLoadingEnd != null)
        {
            OnLoadingEnd();
            OnLoadingEnd = null;
        }
        _loadingBar.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadSceneWithTransition(string sceneName)
    {
        _sceneToLoad = sceneName;
        Singleton.Instance.Transition.Out()
            .AddOutEnd(LoadSceneName);
        AddOnLoadingEnd(Singleton.Instance.Transition.InDefault);

    }
    void LoadSceneName()
    {
        _loadingBar.gameObject.SetActive(true);
        StartCoroutine(LoadAsynchronouslyWithTransition(_sceneToLoad));
    }
    IEnumerator LoadAsynchronouslyWithTransition(string sceneName)
    {
        yield return null;
        Time.timeScale = 0;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/0.9f);
            _loadingBar.Fill(progress);
            yield return null;
        }
        if(OnLoadingEnd != null)
        {
            OnLoadingEnd();
            OnLoadingEnd = null;
        }
        _loadingBar.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
