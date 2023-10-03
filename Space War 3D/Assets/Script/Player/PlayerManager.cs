using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public delegate void PlayerDeathDelegate();
    public event PlayerDeathDelegate OnPlayerDeath;

    public delegate void PlayerGetExp();
    public event PlayerDeathDelegate OnPlayerGetExp;

    public BulletStateManager bulletManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnPlayerDeath?.Invoke();
            Debug.Log(other.name);
            Destroy(gameObject);
        }
        if (other.CompareTag("Exp"))
        {
            OnPlayerGetExp?.Invoke();
        }
    }
}
