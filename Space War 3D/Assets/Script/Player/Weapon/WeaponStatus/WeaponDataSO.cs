using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    public string weaponName;
    public float speed;
    public float damage;
    public float fireRate;

    public Material materialBullet;
}


