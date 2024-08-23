using LP.TurnBasedStrategyTutorial;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAppearsScript : MonoBehaviour
{
    public GameObject player; // Reference to the player
    public float detectionRange = 10f; // Distance at which the enemy appears
    public GameObject SecondEnemy; //To make enemy clear on game scene 
    private bool CanAppear;

    private void Awake()
    {
        SecondEnemy.SetActive(false);
        FindObjectOfType<BattleController>(true).OnBattleFinish += SetCanAppear; 
        
    }

    private void SetCanAppear(BattleController.Target winner)
    {
        if (winner == BattleController.Target.player)
        {
            CanAppear = true;
        }
    }

    void Update()
    {
        
        if (!CanAppear) { return; } 
        float distance = Vector3.Distance(player.transform.position, transform.position);
       

        if (distance <= detectionRange)
        {

            SecondEnemy.SetActive (true); // Enable the enemy
      
        }
        else
        {
            SecondEnemy.SetActive (false); // Disable the enemy
        }

        
    }

}
