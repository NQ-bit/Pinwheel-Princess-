using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour
{
    public void EndCredits()
    {
        SceneManager.LoadScene("Title Scene");
    }
}
