using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterCS : MonoBehaviour
{
    [SerializeField] string nextScene;
    public void OnEnable() {
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }
}
