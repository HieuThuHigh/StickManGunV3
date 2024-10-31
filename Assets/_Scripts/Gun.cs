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
}
