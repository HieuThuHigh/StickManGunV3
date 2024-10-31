using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniature : MonoBehaviour
{
<<<<<<< HEAD:Assets/Animation/Script/Miniature.cs
    private float spinSpeed = 50f; // Tốc độ quay
    // Start is called before the first frame update
    void Start()
=======
    public Transform shootingPoint; // Điểm bắn
    public GameObject bulletPrefab;  // Prefab của đạn
    public float bulletSpeed = 20f;  // Tốc độ đạn
    public List<GunData> guns;       // Danh sách các khẩu súng
    private int _currentBulletCount;   // Số lượng đạn còn lại
    private int _currentGunIndex = 0; // Chỉ số của khẩu súng hiện tại
    public GunData gunData;          // Khẩu súng hiện tại
    private PlayerCustomization _playerCustomization;
    private PlayerController _playerController;

    private void Start()
>>>>>>> 04602a746624713c623636f29dae81ac48237a67:Assets/_Scripts/Bot AutoShooting.cs
    {
        // Khởi tạo số lượng đạn còn lại
        _currentBulletCount = gunData.bulletCount;
        // Lấy PlayerController từ GameObject cha hoặc nơi nào đó
        _playerController = GetComponent<PlayerController>();
        UpdatePlayerGunData();

        // Bắt đầu bắn tự động
        StartCoroutine(AutoShoot());
    }

    private void Update()
    {
<<<<<<< HEAD:Assets/Animation/Script/Miniature.cs
         transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime); // Quay theo trục Y với tốc độ spinSpeed
=======
        // Cập nhật vị trí của shootingPoint
        if (shootingPoint != null)
        {
            Transform playerTransform = transform; // Gán transform của nhân vật
            Vector3 offset = new Vector3(0.5f, -0.3f, 0); // Tạo khoảng cách trước mặt nhân vật
            if (transform.localScale.x < 0)
            {
                offset = new Vector3(-0.5f, -0.3f, 0); // Đổi chiều nếu nhân vật quay trái
            }
            shootingPoint.position = transform.position + offset; // Cập nhật vị trí của shootingPoint
        }

        if (Input.GetKeyDown(KeyCode.Q)) // Kiểm tra nếu người chơi nhấn phím R để nạp lại đạn
        {
            Reload();
        }

        // Thay đổi súng khi nhấn phím số (0-9)
        for (int i = 0; i < guns.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) && i < guns.Count)
            {
                ChangeGun(i);
                break; // Thoát khỏi vòng lặp sau khi thay đổi
            }
        }
    }

    private IEnumerator AutoShoot()
    {
        while (true) // Vòng lặp vô hạn để tự động bắn
        {
            Shoot();
            yield return new WaitForSeconds(0.5f); // Thời gian chờ 0.5 giây
        }
    }

    void UpdatePlayerGunData()
    {
        if (_playerController != null)
        {
            _playerController.UpdateGunData(gunData); // Cập nhật GunData cho PlayerController
        }
    }

    void Shoot()
    {
        if (shootingPoint != null && gunData != null)
        {
            // Kiểm tra nếu còn đạn
            if (_currentBulletCount > 0)
            {
                GameObject bullet = Instantiate(gunData.bulletPrefab, shootingPoint.position, shootingPoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                // Lấy hướng mà viên đạn sẽ bay
                Vector2 shootDirection = transform.right; // Hướng bắn phải
                if (transform.localScale.x < 0)
                {
                    shootDirection = -transform.right; // hướng bắn trái
                }
                // Xoay viên đạn theo hướng bắn
                float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg; // Tính góc dựa trên hướng
                bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // Xoay viên đạn theo hướng bắn

                // Đặt vận tốc cho đạn
                rb.velocity = shootDirection * bulletSpeed;

                StartCoroutine(DestroyBulletAfterDelay(bullet, 1.5f));

                // Giảm số lượng đạn còn lại
                _currentBulletCount--;

                // Kiểm tra nếu hết đạn
                if (_currentBulletCount <= 0)
                {
                    Debug.Log("Hết đạn! Không thể bắn nữa.");
                }
            }
            else
            {
                Debug.Log("Không còn đạn để bắn!"); // Thông báo nếu hết đạn
            }
        }
        else
        {
            Debug.LogError("Shooting Point or Gun Data is not assigned!"); // Thông báo lỗi
        }
    }

    // Hàm để nạp lại đạn
    public void Reload()
    {
        _currentBulletCount = gunData.bulletCount; // Nạp lại số lượng đạn
        Debug.Log("Đạn đã được nạp lại!");
    }

    // Coroutine để xóa viên đạn sau 1,5 giây
    private IEnumerator DestroyBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet); // Xóa viên đạn
    }

    public void ChangeGun(int newGunIndex)
    {
        if (newGunIndex != _currentGunIndex && newGunIndex < guns.Count)
        {
            _currentGunIndex = newGunIndex;
            gunData = guns[_currentGunIndex]; // Cập nhật GunData
            _currentBulletCount = gunData.bulletCount; // Cập nhật số lượng đạn
            UpdatePlayerGunData(); // Cập nhật GunData trong PlayerController
            Debug.Log("Đã thay đổi sang khẩu súng: " + gunData.gunName);
        }
>>>>>>> 04602a746624713c623636f29dae81ac48237a67:Assets/_Scripts/Bot AutoShooting.cs
    }
    
}
