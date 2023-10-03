using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public delegate void EnemyDeathDelegate();
    public event EnemyDeathDelegate OnEnemyDeath;

    [SerializeField] private EnemyStatusSO[] enemyStatusData;
    public int pickRandomEnemy;
    public float health;
    public float damage;
    public float speed;
    public string enemyName;
    public string description;
    public Renderer materialEnemy;

    public void ActivateEnemy()
    {
        pickRandomEnemy = GameManager.Instance.pickRandomEnemy;
        health = enemyStatusData[pickRandomEnemy].health + GameManager.Instance.increaseHealth;
        damage = enemyStatusData[pickRandomEnemy].damage;
        speed = enemyStatusData[pickRandomEnemy].speed + GameManager.Instance.increaseSpeed;
        materialEnemy.material = enemyStatusData[pickRandomEnemy].materialEnemy;
        enemyName = enemyStatusData[pickRandomEnemy].enemyName;
        description = enemyStatusData[pickRandomEnemy].description;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            OnEnemyDeath?.Invoke();
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Bullet"))
        {
            health -= other.GetComponent<BulletBehavior>().demageWeapon;
            GameManager.Instance.Shake(0.5f);
            if (health <= 0)
            {
                GameManager.Instance.OnEnemyDestroyed(gameObject.transform);
                OnEnemyDeath?.Invoke();
                gameObject.SetActive(false);
            }
            // Tindakan yang perlu diambil saat terkena peluru
        }
    }

}
