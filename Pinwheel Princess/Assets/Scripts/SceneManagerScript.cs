using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;



public class SceneManagerScript : MonoBehaviour
{
    private AudioSource AS;
    public GameObject audioSource;

    private void Start()
    {
        AS = audioSource.GetComponent<AudioSource>();
    }


    public void LoadScene(string CutScene)
    {
        SceneManager.LoadScene(CutScene);
        AS.Play();
    }

    public void playButton()
    {
        AS.Play();
    }

}
