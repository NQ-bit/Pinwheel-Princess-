using LP.TurnBasedStrategyTutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    private const int enemyAttack1Damage = 10;
    private const int enemyAttack2Damage = 20; // Example damage value for a second attack
    private const int enemyAttack3Damage = 5; // Example damage value for a third attack

    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public HealthBar healthBar;

    void Start()
    {
       // Enemy2Battle = FindObjectOfType<BattleController>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

}
