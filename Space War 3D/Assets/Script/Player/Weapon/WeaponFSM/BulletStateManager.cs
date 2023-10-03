using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStateManager : MonoBehaviour
{
    [Header("General Settings")]
    public BulletBaseState currentState;

    [Header("Type Weapon")]
    public RapidFireWeapon rapidFireWeapon = new RapidFireWeapon();
    public AreaOfEffectWeapon areaOfEffectWeapon = new AreaOfEffectWeapon();
    public ExplosiveWeapon explosiveWeapon = new ExplosiveWeapon();

    [Header("Movement and Shooting")]
    public Transform[] firePoint;
    public float fireRate;
    public PoolManager poolManager;
    public int pickWeapon;

    public delegate void ShootDelegate();
    public event ShootDelegate OnShootEffect;
    // Start is called before the first frame update
    void Start()
    {
        currentState = rapidFireWeapon;
        currentState.EnterState(this);
    }
    public void ChangeWeapon()
    {
        StopAllCoroutines();
        switch (pickWeapon)
        {
            case 0: // Misalnya, jika pickRandomEnemy adalah 0, maka gunakan HarakiriBot
                currentState = rapidFireWeapon;
                break;
            case 1: // Jika pickRandomEnemy adalah 1, maka gunakan GunBot
                currentState = areaOfEffectWeapon;
                break;
            case 2: // Jika pickRandomEnemy adalah 1, maka gunakan GunBot
                currentState = explosiveWeapon;
                break;
        }       
        currentState.EnterState(this);
    }

    public void SwitchState(BulletBaseState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;
        currentState.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            GameManager.Instance.OnGetWeapon();
            pickWeapon = other.GetComponent<WeaponItemBehavior>().weaponChoiceToInstance;
            currentState.ExitState(this);
            ChangeWeapon();
        }
    }

}
