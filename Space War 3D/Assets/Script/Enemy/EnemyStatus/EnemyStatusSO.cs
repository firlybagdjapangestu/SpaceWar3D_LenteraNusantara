using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStatus", menuName = "Enemy Status")]
public class EnemyStatusSO : ScriptableObject
{
    [Header("Status Musuh")]
    [Tooltip("Tingkat kesehatan musuh.")]
    public float health = 100f;

    [Tooltip("Tingkat kerusakan yang dapat dihasilkan oleh musuh.")]
    public float damage = 10f;

    [Tooltip("Kecepatan pergerakan musuh.")]
    public float speed = 5f;

    [Tooltip("Kecepatan pergerakan musuh.")]
    public Material materialEnemy;

    [Header("Deskripsi Musuh")]
    [Tooltip("Nama musuh.")]
    public string enemyName = "Musuh";

    [TextArea(3, 10)]
    [Tooltip("Deskripsi musuh.")]
    public string description = "Ini adalah musuh dalam permainan.";

    [Header("Kode Unik")]
    [Tooltip("Kode unik untuk setiap musuh.")]
    public string enemyCode = "UniqueCode123";
}
