using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyStateManager : MonoBehaviour
{
    [Header("General Settings")]
    public EnemyBaseState currentState;
    public EnemyManager enemyStatus;

    [Header("Enemy Types")]
    private int pickRandomEnemy;
    public HarakiriBot harakiriBot = new HarakiriBot();
    public GunBot gunBot = new GunBot();

    [Header("Movement and Shooting")]
    public Transform[] stayPosition;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public PoolManager poolManager;
    public Material bulletEnemyMaterial;
    public float fireRate;

    public enum EnemyState
    {
        HarakiriBot,
        GunBot,
    }

    public IEnumerator AutoShoot()
    {
        GameObject bulletPool = poolManager.GetBulletPooled();

        while (true)
        {
            if (bulletPool != null)
            {
                GameManager.Instance.OnShootEffect();
                bulletPool.GetComponent<Renderer>().material = bulletEnemyMaterial;
                bulletPool.GetComponent<BulletBehavior>().speedWeapon = -bulletPool.GetComponent<BulletBehavior>().speedWeapon;
                bulletPool.tag = "Enemy";
                bulletPool.transform.position = firePoint.position;
                bulletPool.transform.rotation = firePoint.rotation;
                bulletPool.SetActive(true);
                bulletPool.GetComponent<BulletBehavior>().StartMoveBullet();
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void ActivateEnemy()
    {
        currentState = null;
        stayPosition = GameManager.Instance.stayPosition;
        pickRandomEnemy = GameManager.Instance.pickRandomEnemy;
        poolManager = GameObject.Find("Pool Manager").GetComponent<PoolManager>();
        switch (pickRandomEnemy)
        {
            case 0: 
                currentState = harakiriBot;
                break;
            case 1: 
                currentState = gunBot;
                break;
                
        }

        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;
        currentState.EnterState(this);
    }
}
