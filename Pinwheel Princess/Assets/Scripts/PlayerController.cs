using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Animator Animator;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    public int maxHealth = 100;
    public int currentHealth { get; private set; } 

    public HealthBar healthBar;
    

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("PlayerDeathScene");
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    void Start()
    {
        currentHealth = maxHealth;
       healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void GainHealth(int Health)
    {
        currentHealth = Mathf.Clamp(currentHealth+Health, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

    }

    private void Move()
    {
        rb.MovePosition(rb.position +   movement * (moveSpeed * Time.fixedDeltaTime));
    }

    public void PlayAttack()
    {
        Animator.SetTrigger("Attack");
    }

    public void PlayHeal()
    {
        Animator.SetTrigger("Heal"); 
    }
}
