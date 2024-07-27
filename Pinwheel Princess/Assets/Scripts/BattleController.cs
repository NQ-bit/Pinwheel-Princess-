using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace LP.TurnBasedStrategyTutorial
{
    public class BattleController : MonoBehaviour
    {
        [SerializeField] private PlayerController player = null;
        [SerializeField] private EnemyController enemy = null;
        [SerializeField] private Button attackBtn = null;
        [SerializeField] private Button healBtn = null;
        public Action<Target> OnBattleFinish; 
        private Coroutine changeTurnCoroutine = null;

        public enum Target { player,  enemy }

        private void Awake()
        {
            attackBtn.onClick.AddListener(BtnAttack);
            healBtn.onClick.AddListener(BtnHeal);
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

        private void Attack(Target target, int damage)
        {
            if(target == Target.enemy)
            {
                enemy.TakeDamage ( damage);
                player.PlayAttack();
            }
            else
            {
                player.TakeDamage( damage);
            }

            ChangeTurn();
        }



        private void Heal(Target target, int amount)
        {
            if (target == Target.enemy)
            {
                enemy.GainHealth(amount);
                
            }
            else
            {
                player.GainHealth(amount);
                player.PlayHeal();
            }

            ChangeTurn();
        }

        private void BtnAttack()
        {
            Attack(Target.enemy, 10);
        }

        public void BtnHeal()
        {
            Heal(Target.player, 5);
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
            isPlayerTurn = !isPlayerTurn;

            yield return new WaitForSeconds(1);

            var playerhealth = player.currentHealth; 
            var enemyhealth = enemy.currentHealth;

            if (playerhealth <= 0 )
            {
                EndBattle (Target.enemy);
           
            }
            else if (enemyhealth <= 0 ) 
            {
                EndBattle(Target.player); 
            }
            else
            {
                if (!isPlayerTurn)
                {
                    StartCoroutine(EnemyTurn());
                }
                else
                {
                    attackBtn.interactable = true;
                    healBtn.interactable = true;
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
    }

}

