using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneController.instance.LoadSceneAsync(SceneController.instance.NextScene);
    }
}
