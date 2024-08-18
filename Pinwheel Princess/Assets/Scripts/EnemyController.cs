using LP.TurnBasedStrategyTutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private const int enemyAttack1Damage = 12;
    private const int enemyAttack2Damage = 15; // Example damage value for a second attack
    private const int enemyAttack3Damage = 20; // Example damage value for a third attack
    private BattleController battleController;

    public DialogueManager dialogueManager;
    public Animator animator;


    // Example usage in your EnemyController script:
    public void StartEnemyDialogue(string[] dialogueLines)
    {
        dialogueManager.StartDialogue(dialogueLines);
    }


    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public HealthBar healthBar;

    public void SetUp(BattleController Controller)
    {
        battleController = Controller;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("PlayerWinScene");
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void GainHealth(int Health)
    {
        currentHealth = Mathf.Clamp(currentHealth + Health, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    private IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(3);

        int random = UnityEngine.Random.Range(1, 4); // Adjust the range to include the new attacks

        //Triggers Animation
        animator.SetTrigger("SeaMonkey");

        if (random == 1)
        {
            battleController.Attack(BattleController.Target.player, enemyAttack1Damage);
        }
        else if (random == 2)
        {
            battleController.Attack(BattleController.Target.player, enemyAttack2Damage);
        }
        else if (random == 3)
        {
            battleController.Attack(BattleController.Target.player, enemyAttack3Damage);
        }
        else
        {
            battleController.Heal(BattleController.Target.enemy, 3);
        }
    }

}
