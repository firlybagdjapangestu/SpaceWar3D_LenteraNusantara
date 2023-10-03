using System.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float limitForward;
    [SerializeField] private float limitBackward;
    [SerializeField] private WeaponDataSO[] weaponData;
    [SerializeField] private Renderer bulletRenderer;
    public float speedWeapon;
    public float demageWeapon;
    public float fireRateWeapon;

    public void ChangeWeapon(int pickWeapon)
    {
        speedWeapon = weaponData[pickWeapon].speed;
        demageWeapon = weaponData[pickWeapon].damage;
        fireRateWeapon = weaponData[pickWeapon].fireRate;
        bulletRenderer.material = weaponData[pickWeapon].materialBullet;
        StartCoroutine(MoveBullet());
    }

    public void StartMoveBullet()
    {
        StartCoroutine(MoveBullet());
    }

    public IEnumerator MoveBullet()
    {
        while (true)
        {
            transform.Translate(Vector3.forward * speedWeapon * Time.deltaTime);

            if (transform.position.z >= limitForward || transform.position.z <= limitBackward)
            {
                gameObject.SetActive(false);
                gameObject.tag = "Bullet";
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == gameObject.tag)
        {
            return;
        }
        gameObject.SetActive(false);
    }
}
