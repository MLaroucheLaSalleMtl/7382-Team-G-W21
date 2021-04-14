using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public string NextScene;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    /// <summary>
    /// Asynchronous loading
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(SceneAsyncRoutine(sceneName));
    }

    /// <summary>
    /// Coroutien to handle the asynchronous loading
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator SceneAsyncRoutine(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(currentScene);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (LoadingPanelManager.instance != null)
                LoadingPanelManager.instance.UpdateSlider(async.progress);

            if (async.progress >= 0.9f)
            {
                if (LoadingPanelManager.instance != null)
                    LoadingPanelManager.instance.UpdateSlider(1f);

                async.allowSceneActivation = true;
                yield return null;
            }
        }
        yield return null;
    }
}
