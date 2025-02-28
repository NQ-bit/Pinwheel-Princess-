using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBarScene : MonoBehaviour
{

    [SerializeField] string nextScene;
    [SerializeField] Slider loadSlider;

    public void Start() {
        LoadScene(nextScene);
    }

    void LoadScene (string sceneName) {
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync (string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadSlider.value = progress;
            yield return null;
        }
    }
}
