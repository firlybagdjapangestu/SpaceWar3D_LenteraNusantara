using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffectWeapon : BulletBaseState
{
    public override void EnterState(BulletStateManager bullet)
    {
        Debug.Log("Mode : Area of Effect");
        bullet.firePoint[1].rotation = Quaternion.Euler(0, 10, 0);
        bullet.firePoint[2].rotation = Quaternion.Euler(0, -10, 0);
        for (int i = 0; i <= GameManager.Instance.levelPlayer; i++)
        {
            bullet.StartCoroutine(AutoShoot(bullet, i));
        }
    }

    public override void ExitState(BulletStateManager bullet)
    {
        bullet.firePoint[1].rotation = Quaternion.Euler(0, 0, 0);
        bullet.firePoint[2].rotation = Quaternion.Euler(0, 0, 0);
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
