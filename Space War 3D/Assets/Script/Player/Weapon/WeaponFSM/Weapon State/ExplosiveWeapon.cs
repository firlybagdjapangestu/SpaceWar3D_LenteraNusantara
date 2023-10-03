using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveWeapon : BulletBaseState
{
    public override void EnterState(BulletStateManager bullet)
    {
        Debug.Log("Mode : Explosive Weapon");
        bullet.StartCoroutine(AutoShoot(bullet, 0));
    }

    public override void ExitState(BulletStateManager bullet)
    {
        bullet.StopAllCoroutines();
        bullet.currentState = null;
    }

    public override void OnTriggerState(BulletStateManager bullet, Collider other)
    {
        
    }

    public override void UpdateState(BulletStateManager bullet)
    {
        
    }

    IEnumerator AutoShoot(BulletStateManager bullet, int firePointIndex)
    {
        while (true)
        {
            GameObject bulletPool = bullet.poolManager.GetBulletPooled();
            GameManager.Instance.OnShootEffect();
            if (bulletPool != null)
            {
                bullet.fireRate = bulletPool.GetComponent<BulletBehavior>().fireRateWeapon;
                bulletPool.transform.position = bullet.firePoint[firePointIndex].position;
                bulletPool.transform.rotation = bullet.firePoint[firePointIndex].rotation;
                bulletPool.SetActive(true);
                bulletPool.GetComponent<BulletBehavior>().ChangeWeapon(bullet.pickWeapon);
            }
            yield return new WaitForSeconds(bullet.fireRate);
        }
    }

}
