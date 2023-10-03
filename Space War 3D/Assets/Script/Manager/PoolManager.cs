using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [Header("Pool Enemy")]
    private List<GameObject> pooledEnemy = new List<GameObject>();
    private int amountEnemy = 6;
    [SerializeField] private GameObject enemyPrefabs;
    [SerializeField] private Transform poolOfEnemy;

    [Header("Pool Bullet")]
    private List<GameObject> pooledBullet = new List<GameObject>();
    private int amountBullet = 20;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform poolOfBullet;

    [Header("Pool Weapon")]
    private List<GameObject> pooledWeaponItem = new List<GameObject>();
    private int amountWeapon = 5;
    [SerializeField] private GameObject weaponItemPrefabs;
    [SerializeField] private Transform poolOfWeapon;

    [Header("Pool Exp")]
    private List<GameObject> pooledExp = new List<GameObject>();
    private int amountExp = 20;
    [SerializeField] private GameObject expPrefabs;
    [SerializeField] private Transform poolOfExp;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        for (int i = 0; i < amountEnemy; i++)
        {
            GameObject enemyClone = Instantiate(enemyPrefabs);
            enemyClone.transform.SetParent(poolOfEnemy);
            enemyClone.SetActive(false);
            pooledEnemy.Add(enemyClone);
        }
        for (int i = 0; i < amountBullet; i++)
        {
            GameObject bulletClone = Instantiate(bulletPrefabs);
            bulletClone.transform.SetParent(poolOfBullet);
            bulletClone.SetActive(false);
            pooledBullet.Add(bulletClone);
        }
        for (int i = 0; i < amountWeapon; i++)
        {
            GameObject weaponClone = Instantiate(weaponItemPrefabs);
            weaponClone.transform.SetParent(poolOfWeapon);
            weaponClone.SetActive(false);
            pooledWeaponItem.Add(weaponClone);
        }
        for (int i = 0; i < amountExp; i++)
        {
            GameObject expClone = Instantiate(expPrefabs);
            expClone.transform.SetParent(poolOfExp);
            expClone.SetActive(false);
            pooledExp.Add(expClone);
        }


    }

    public GameObject GetEnemyPooled()
    {
        for (int i = 0; i < pooledEnemy.Count; i++)
        {
            if (!pooledEnemy[i].activeInHierarchy)
            {
                return pooledEnemy[i];
            }
        }
        return null;
    }

    public GameObject GetBulletPooled()
    {
        for (int i = 0; i < pooledBullet.Count; i++)
        {
            if (!pooledBullet[i].activeInHierarchy)
            {
                return pooledBullet[i];
            }
        }
        return null;
    }

    public GameObject GetWeaponPooled()
    {
        for (int i = 0; i < pooledWeaponItem.Count; i++)
        {
            if (!pooledWeaponItem[i].activeInHierarchy)
            {
                pooledWeaponItem[i].GetComponent<WeaponItemBehavior>().InstanceWeapon();
                return pooledWeaponItem[i];
            }
        }
        return null;
    }

    public GameObject GetExpPooled()
    {
        for (int i = 0; i < pooledExp.Count; i++)
        {
            if (!pooledExp[i].activeInHierarchy)
            {
                return pooledExp[i];
            }
        }
        return null;
    }
}
