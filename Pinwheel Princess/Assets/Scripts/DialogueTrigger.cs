using LP.TurnBasedStrategyTutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public string[] dialogueLines;
    [SerializeField] private bool battleAfterDialogue;
    [SerializeField] private EnemyController enemyPrefab; 

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<PlayerMainGameControllers>()) 
            { 
            dialogueManager.StartDialogue(dialogueLines);
            if (!battleAfterDialogue)
            {
                return;
            }

            dialogueManager.OnDialogueFinish += StartBattleAfterDialogue; 
        }


    }

    private void StartBattleAfterDialogue()
    {

        dialogueManager.OnDialogueFinish -= StartBattleAfterDialogue;
       var BattleController = FindObjectOfType<BattleController>(true);
        BattleController.StartBattle(enemyPrefab);

    }
}
