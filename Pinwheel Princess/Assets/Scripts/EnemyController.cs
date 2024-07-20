using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; }

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("EnemyDeathScene");
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
}
