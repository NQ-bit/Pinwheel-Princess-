using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPerivousScene : MonoBehaviour
{
    private Stack<int> sceneHistory = new Stack<int>();

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(int sceneIndex)
    {
        sceneHistory.Push(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadPreviousScene()
    {
        if (sceneHistory.Count > 0)
        {
            int previousSceneIndex = sceneHistory.Pop();
            SceneManager.LoadScene(previousSceneIndex);
        }
        else
        {
            Debug.LogError("No previous scene in history.");
        }
    }
}
