using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;



public class SceneManagerScript : MonoBehaviour
{
    

    public void LoadScene(string CutScene)
    {
        SceneManager.LoadScene(CutScene);
    }
}
