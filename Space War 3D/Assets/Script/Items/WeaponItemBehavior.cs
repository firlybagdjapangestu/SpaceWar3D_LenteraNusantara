using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemBehavior : MonoBehaviour
{
    public Renderer weaponItemRenderer;
    public Material[] materialWeapon;
    public float speedDropDwon;
    public int howMuchWeapon;
    public int weaponChoiceToInstance;
    [SerializeField] private float limitDistanceWeaponItem;
    private void Start()
    {
        InstanceWeapon();
    }
    public void InstanceWeapon()
    {
        weaponChoiceToInstance = Random.Range(0, howMuchWeapon);
        weaponItemRenderer.material = materialWeapon[weaponChoiceToInstance];
    }
    private void Update()
    {
        transform.Translate(Vector3.back * speedDropDwon * Time.deltaTime);
        if (transform.position.z <= limitDistanceWeaponItem)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
