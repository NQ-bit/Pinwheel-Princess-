using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private string[] textLines;
    [SerializeField] private float textSpeed;
    [SerializeField] private float textLineSpeed;

    private int index;
    private int indexMax;
    private bool autoplay = false;

    public bool textEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        textBox.text = string.Empty;
        StartText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("a")) {
            SetAutoplay();
            Debug.Log("Autoplay: " + autoplay);
        }
        if(autoplay)
        {
            if(textBox.text == textLines[index])
                {
                    NextLine();
                }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            if(textBox.text == textLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textBox.text = textLines[index];
            }
        }
        if (index == textLines.Length) {
            textEnd = true;
        }
    }
    void StartText()
    {
        index = 0;
        StartCoroutine(TextLineType());
    }

    IEnumerator TextLineType()
    {
        if (autoplay) {
            yield return new WaitForSeconds(textLineSpeed);
        }
        textBox.text = string.Empty;
        foreach (char c in textLines[index].ToCharArray())
        {
            indexMax = textLines[index].Length;
            textBox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < textLines.Length - 1)
        {
            index++;
            StartCoroutine(TextLineType());
        }
    }

    public void SetAutoplay()
    {
        autoplay = !autoplay;
        /*if (Input.GetMouseButtonDown(1)) {
            Debug.Log("Autoplay button");
            autoplay = !autoplay;
        }*/
    }
}
