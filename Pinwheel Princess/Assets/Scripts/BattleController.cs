using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;
using DG.Tweening;

namespace LP.TurnBasedStrategyTutorial
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private PlayerController player = null;
        [SerializeField] private EnemyController enemy = null;
        [SerializeField] private Button attackBtn = null;
        [SerializeField] private Button attack2Btn = null;
        [SerializeField] private Button attack3Btn = null;
        [SerializeField] private Button healBtn = null;
        [SerializeField] private DamagePopUp PlayerDamagePopUp = null;
        [SerializeField] private DamagePopUp EnemyDamagePopUp = null;
        [SerializeField] private DamagePopUp PlayerScore = null;
        [SerializeField] private DamagePopUp EnemyScore = null;
        [SerializeField] private TextMeshProUGUI attackCooldownText = null;
        [SerializeField] private TextMeshProUGUI attack2CooldownText = null;
        [SerializeField] private TextMeshProUGUI attack3CooldownText = null;
        [SerializeField] private TextMeshProUGUI healCooldownText = null;

        public Action<Target> OnBattleFinish;
        private Coroutine changeTurnCoroutine = null;

        private int attackCooldown = 0;
        private int attack2Cooldown = 0;
        private int attack3Cooldown = 0;
        private int healCooldown = 0;

        private int playerScore = 0; // Add a variable to keep track of the player's score

        public enum Target { player, enemy }

        private void Awake()
        {
            attackBtn.onClick.AddListener(BtnAttack);
            healBtn.onClick.AddListener(BtnHeal);
            attack2Btn.onClick.AddListener(Attack2Btn);
            attack3Btn.onClick.AddListener(Attack3Btn);

        }

        public void StartBattle()
        {
            isPlayerTurn = true;
            gameObject.SetActive(true);
        }

        void EndBattle(Target Winner)
        {
            OnBattleFinish?.Invoke(Winner);
            gameObject.SetActive(false);
        }

        private bool isPlayerTurn = true;

        public void Attack(Target target, int damage)
        {
            if (target == Target.enemy)
            {
                enemy.TakeDamage(damage);
                player.PlayAttack();
                EnemyDamagePopUp.Setup(damage);
            }
            else
            {
                player.TakeDamage(damage);
                PlayerDamagePopUp.Setup(damage);
            }

            playerScore += damage; // Update the player's score
            PlayerScore.Setup(playerScore); // Display the player's score
            ChangeTurn();
        }

        public void Attack2Btn()
        {
            if (attack2Cooldown <= 1)
            {
                Attack(Target.enemy, 15); // Example damage value for Attack2
                attack2Cooldown = 1;
            }
            else
            {
                ShowCooldownMessage(attack2CooldownText);
            }
        }

        public void Attack3Btn()
        {
            if (attack3Cooldown <= 0)
            {
                Attack(Target.enemy, 20); // Example damage value for Attack3
                attack3Cooldown = 3; //Cooldown for 3 turns 
            }
            else
            {
                ShowCooldownMessage(attack3CooldownText);
            }
        }

        public void Heal(Target target, int amount)
        {
            if (target == Target.enemy)
            {
                enemy.GainHealth(amount);
                EnemyDamagePopUp.Setup(amount, false);

            }
            else
            {
                player.GainHealth(amount);
                player.PlayHeal();
                PlayerDamagePopUp.Setup(amount, false);

            }

            ChangeTurn();
        }
        
        public void BtnAttack()
        {
            if (attackCooldown <= 1)
            {
                Attack(Target.enemy, 10);
                attackCooldown = 3; 
            }
            else
            {
                ShowCooldownMessage(attackCooldownText);
            }
            
        }

        public void BtnHeal()
        {
            if (healCooldown <= 0)
            {
                Heal(Target.player, 5);
                healCooldown = 3; // Cooldown for 3 turns
            }
            else
            {
                ShowCooldownMessage(healCooldownText);
            }
            
        }

        private void ChangeTurn()

        {
            if (changeTurnCoroutine != null)
            {
                return; 
            }
           changeTurnCoroutine = StartCoroutine(ChangeTurnProcess());
        }

        private IEnumerator ChangeTurnProcess()
        {
            attackBtn.interactable = false;
            healBtn.interactable = false;
            attack2Btn.interactable = false;
            attack3Btn.interactable = false;
            isPlayerTurn = !isPlayerTurn;

            yield return new WaitForSeconds(1);

            var playerhealth = player.currentHealth; 
            var enemyhealth = enemy.currentHealth;

            if (playerhealth <= 0 )
            {
                EndBattle (Target.enemy);
                PlayerDamagePopUp.Setup (playerhealth);
           
            }
            else if (enemyhealth <= 0 ) 
            {
                EndBattle(Target.player); 
                PlayerDamagePopUp.Setup (enemyhealth);
            }
            else
            {
                if (!isPlayerTurn)
                {
                    StartCoroutine(EnemyTurn());
                }
                else
                {
                    if (attackCooldown > 0) attackCooldown--;
                    if (attack2Cooldown > 0) attack2Cooldown--;
                    if (attack3Cooldown > 0) attack3Cooldown--;
                    if (healCooldown > 0) healCooldown--;

                    attackBtn.interactable = attackCooldown <= 0;
                    healBtn.interactable = healCooldown <= 0;
                    attack2Btn.interactable= attackCooldown <= 0;
                    attack3Btn.interactable = attack3Cooldown <= 0;

                    // Update the cooldown text
                    attackCooldownText.text = "Can Not Play: " + attackCooldown;
                    healCooldownText.text = "Can Not Play: " + healCooldown;
                    attack2CooldownText.text = "Can Not Play: " + attack2Cooldown;
                    attack3CooldownText.text = "Can Not Play: " + attack3Cooldown;
                }
            }
           
            changeTurnCoroutine = null;

        }



        private IEnumerator EnemyTurn()
        {
            yield return new WaitForSeconds(3);

            int random = 0;
            random = UnityEngine.Random.Range(1, 3);

            if (random == 1)
            {
                Attack(Target.player, 12);
            }
            else
            {
                Heal(Target.enemy, 3);
            } 
        }

        private void ShowCooldownMessage(TextMeshProUGUI cooldownText)
        {
            cooldownText.text = "You can't use at this time.";
            cooldownText.gameObject.SetActive(true);
            cooldownText.DOFade(1, 0.5f).OnComplete(() =>
            {
                cooldownText.DOFade(0, 0.5f).SetDelay(2).OnComplete(() =>
                {
                    cooldownText.gameObject.SetActive(false);
                });
            });
        }
    }

}

