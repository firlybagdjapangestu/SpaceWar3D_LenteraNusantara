using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GunBot : EnemyBaseState
{
    private Transform targetPosition;
    private bool isMoving = false;

    public override void EnterState(EnemyStateManager enemy)
    {
        targetPosition = enemy.stayPosition[GameManager.Instance.currentEnemy];
        enemy.StartCoroutine(MoveToPositionAfterDelay(enemy, 2f));
    }

    public override void ExitState(EnemyStateManager enemy)
    {
        
    }

    public override void OnCollisionState(EnemyStateManager enemy)
    {
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (isMoving) return;
    }

   
    private IEnumerator MoveToPositionAfterDelay(EnemyStateManager enemy, float delay)
    {
        isMoving = true;
        while (Vector3.Distance(enemy.transform.position, targetPosition.position) > 0.1f)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition.position, enemy.enemyStatus.speed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(delay);
        isMoving = false;
        enemy.StartCoroutine(enemy.AutoShoot());
    }
    
}
