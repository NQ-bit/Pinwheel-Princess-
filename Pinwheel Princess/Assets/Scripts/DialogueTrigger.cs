using LP.TurnBasedStrategyTutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public string[] dialogueLines;
    [SerializeField] private bool battleAfterDialogue;
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private Sprite BackgroundSprite; 
    enum AfterBattle { EnemyDies, WinGame}
    [SerializeField] private AfterBattle afterBattle;
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
        BattleController.StartBattle(enemyPrefab, BackgroundSprite);
        BattleController.OnBattleFinish += AfterBattleAction; 
    }

    private void AfterBattleAction (BattleController.Target winner)
    {
        var BattleController = FindObjectOfType<BattleController>(true);
        BattleController.OnBattleFinish -= AfterBattleAction;

        if (winner == BattleController.Target.player)
        {
            switch (afterBattle)
            {
                case AfterBattle.EnemyDies:
                    Destroy(gameObject); break; 
                    case AfterBattle.WinGame:
                    SceneManager.LoadScene("PlayerWinScene"); break; 
            }
        }
    }
}
