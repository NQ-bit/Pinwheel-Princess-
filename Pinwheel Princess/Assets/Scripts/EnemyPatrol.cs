using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{ 
    //Delete this 

    public float speed = 2f;
    public float patrolTime = 2f;
    private Vector2 patrolAreaMin;
    private Vector2 patrolAreaMax;
    private Vector2 targetPosition;
    private float patrolTimer;

    void Start()
    {
        BoxCollider2D patrolArea = GetComponent<BoxCollider2D>();
        patrolAreaMin = patrolArea.bounds.min;
        patrolAreaMax = patrolArea.bounds.max;
        SetNewTargetPosition();
    }

    void Update()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolTime)
        {
            SetNewTargetPosition();
            patrolTimer = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void SetNewTargetPosition()
    {
        float randomX = Random.Range(patrolAreaMin.x, patrolAreaMax.x);
        float randomY = Random.Range(patrolAreaMin.y, patrolAreaMax.y);
        targetPosition = new Vector2(randomX, randomY);
    }
}
