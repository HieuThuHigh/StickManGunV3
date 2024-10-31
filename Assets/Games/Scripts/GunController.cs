using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform shootingPoint; // Điểm bắn
    public GameObject bulletPrefab;  // Prefab của đạn
    public float bulletSpeed = 20f;  // Tốc độ đạn

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Kiểm tra nếu người chơi nhấn phím bắn
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (shootingPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Lấy hướng mà viên đạn sẽ bay
            Vector2 shootDirection = shootingPoint.right; // Hướng bắn

            // Đặt vận tốc cho đạn
            rb.velocity = shootDirection * bulletSpeed;
        }
        else
        {
            Debug.LogError("Shooting Point is not assigned!"); // Thông báo lỗi nếu chưa gán
        }
    }
}


