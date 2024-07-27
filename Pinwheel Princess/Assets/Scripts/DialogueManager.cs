using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using LP.TurnBasedStrategyTutorial;
using TMPro; 
using System;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
   // public Text dialogueText;
    public GameObject dialogueBox;
    private Queue<string> sentences;
    public TextMeshProUGUI DialogueText;
    public GameObject fightPrefab;
    public Action OnDialogueFinish; 

    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            NextLine();
        }
    }

    public void StartDialogue(string[] dialogueLines)
    {
        dialogueBox.SetActive(true);
        sentences.Clear();

        foreach (string line in dialogueLines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        DialogueText.text = sentence;
    }

    private void NextLine()
    {
       if (sentences.Count > 0)
        {
            DisplayNextSentence();
        }
       else
        {
            EndDialogue();
            fightPrefab.SetActive(true);
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        
        OnDialogueFinish?.Invoke();
    }
}
