using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    public void StartGameFromBeginning()
    {
        SceneManager.LoadScene("Title Scene");
    }

    public void FromBeginning()
    {
        SceneManager.LoadScene("Credits");
    }
}
