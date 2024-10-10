using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.Progress;

public class GunController : MonoBehaviour
{
    public Transform shootingPoint; // Điểm bắn
    public GameObject bulletPrefab;  // Prefab của đạn
    public float bulletSpeed = 20f;  // Tốc độ đạn
    public List<GunData> guns;       // Danh sách các khẩu súng
   // private int currentGunIndex = 0; // Chỉ số của khẩu súng hiện tại
    private int currentBulletCount;   // Số lượng đạn còn lại
    public GunData gunData;          // Khẩu súng hiện tại
    private PlayerCustomization PlayerCustomization;
    private PlayerController playerController ;  
    void Start()
    {
        // Gán vị trí của shootingPoint bằng gunPosition
        if (shootingPoint != null)
        {
           // shootingPoint.position = PlayerCustomization.gunPosition;  // Gán vị trí bắn
        }
                                                                       // Khởi tạo số lượng đạn còn lại
            currentBulletCount = gunData.bulletCount;
    }
    void Update()
    {
        // Cập nhật vị trí của shootingPoint
        if (shootingPoint != null)
        {
            // Giả sử player là Transform của nhân vật
            Transform playerTransform = transform; // Gán transform của nhân vật (hoặc bạn có thể chỉ định đối tượng nhân vật cụ thể)

            // Đặt vị trí của shootingPoint ở trước mặt player
            shootingPoint.position = playerTransform.position + playerTransform.right * 0.5f + Vector3.down * 0.3f; /// 1.0f là khoảng cách từ player đến shootingPoint
            shootingPoint.rotation = playerTransform.rotation; // Đặt hướng của shootingPoint  giống hướng của player
        }



        if (Input.GetButtonDown("Fire1")) // Kiểm tra nếu người chơi nhấn phím bắn
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Q)) // Kiểm tra nếu người chơi nhấn phím R để nạp lại đạn
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (shootingPoint != null && gunData != null)
        {
            // Kiểm tra nếu còn đạn
            if (currentBulletCount > 0)
            {
                //for (int i = 0; i < gunData.bulletCount; i++)
                //{
                    // GameObject bullet = Instantiate(gunData.bulletPrefab, shootingPoint.position, shootingPoint.rotation);
                    GameObject bullet = Instantiate(gunData.bulletPrefab, shootingPoint.position, shootingPoint.rotation);

                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                    // Lấy hướng mà viên đạn sẽ bay
                     Vector2 shootDirection = shootingPoint.right; // Hướng bắn
                 
                   

                    // Đặt vận tốc cho đạn
                    rb.velocity = shootDirection * bulletSpeed;

               
                    StartCoroutine(DestroyBulletAfterDelay(bullet, 1.5f));
               // }

                // Giảm số lượng đạn còn lại
                currentBulletCount--;


                // Kiểm tra nếu hết đạn
                if (currentBulletCount <= 0)
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
        currentBulletCount = gunData.bulletCount; // Nạp lại số lượng đạn
        Debug.Log("Đạn đã được nạp lại!");
    }
    // Coroutine để xóa viên đạn sau 1,5 giây
    private IEnumerator DestroyBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet); // Xóa viên đạn
    }

}


