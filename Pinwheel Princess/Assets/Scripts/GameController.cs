using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LP.TurnBasedStrategyTutorial
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerController player = null;
        [SerializeField] private EnemyController enemy = null;
        [SerializeField] private Button attackBtn = null;
        [SerializeField] private Button healBtn = null;
        public Text playerHealthText; // Reference to the player's health text

        private enum Target { player,  enemy }

        private void Awake()
        {
            attackBtn.onClick.AddListener(BtnAttack);
            healBtn.onClick.AddListener(BtnHeal);
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
            isPlayerTurn = !isPlayerTurn;

            if(!isPlayerTurn)
            {
                attackBtn.interactable = false;
                healBtn.interactable = false;

                StartCoroutine(EnemyTurn());
            }
            else
            {
                attackBtn.interactable = true;
                healBtn.interactable = true;
            }
        }

        private IEnumerator EnemyTurn()
        {
            yield return new WaitForSeconds(3);

            int random = 0;
            random = Random.Range(1, 3);

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

