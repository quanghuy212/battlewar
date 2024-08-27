using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public bool isChosen = false; // Thuộc tính isChosen
    public float moveSpeed = 5f;  // Tốc độ di chuyển của player
    private Transform target;     // Mục tiêu hiện tại của player

    void Update()
    {
        Debug.Log("Player time!");
        if (isChosen)
        {
            Debug.Log("Player is chosen!");
        }
        else
        {
            FindAndMoveToClosestEnemy();
            Debug.Log("Player is not  chosen!");
        }
        
    }

    void FindAndMoveToClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        // Tìm kẻ địch gần nhất
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        // Di chuyển đến vị trí kẻ địch gần nhất
        if (closestEnemy != null)
        {
            target = closestEnemy.transform;
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
