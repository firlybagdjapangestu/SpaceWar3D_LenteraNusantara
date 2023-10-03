using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarakiriBot : EnemyBaseState
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
        enemy.currentState = null;
    }

    public override void OnCollisionState(EnemyStateManager enemy)
    {
        // Logika tindakan saat berkolisi
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (isMoving) return;
        enemy.transform.Translate(Vector3.back * enemy.enemyStatus.speed * Time.deltaTime);
        if(enemy.transform.position.z <= -120)
        {
            enemy.gameObject.SetActive(false);
            GameManager.Instance.enemyDestroyed++;
        }
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
    }
}
