using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGunData", menuName = "Gun Data", order = 1)]
public class GunData : ScriptableObject
{

    public string gunName;           // Tên súng
    public Sprite Spritegun;          // sprite sung
    public GameObject bulletPrefab;   // Prefab của đạn
    public float bulletSpeed;         // Tốc độ đạn
    public int damage;                // Sát thương
    public int bulletCount;           // Số lượng đạn
    // Phương thức để thiết lập dữ liệu súng từ GunData
    // public void SetGunData(GunData gunData)
    // {
    //     gunName = gunData.gunName;
    //     Spritegun = gunData.Spritegun;
    //     bulletPrefab = gunData.bulletPrefab;
    //     bulletSpeed = gunData.bulletSpeed;
    //     damage = gunData.damage;
    //     bulletCount = gunData.bulletCount;

    // }
    }
