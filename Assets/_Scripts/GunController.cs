using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunController : MonoBehaviour
{

    public Transform shootingPoint; // Điểm bắn
    public GameObject bulletPrefab;  // Prefab của đạn
    public SpriteRenderer gunSpriteRenderer; // Thêm thành phần này để tham chiếu tới SpriteRenderer

    public float bulletSpeed = 20f;  // Tốc độ đạn
    public List<GunData> guns;       // Danh sách các khẩu súng
                                     // private int currentGunIndex = 0; // Chỉ số của khẩu súng hiện tại
    private int _currentBulletCount;   // Số lượng đạn còn lại
    private int _currentGunIndex = 0; // Chỉ số của khẩu súng hiện tại
    public GunData gunData;          // Khẩu súng hiện tại
    public Text bulletCountText;
    private PlayerCustomization _playerCustomization;
    private PlayerController _playerController;
    void Start()
    {
        if (guns.Count > 0)
        {
            ChangeGun(_currentGunIndex); // Đặt khẩu súng đầu tiên khi bắt đầu
        }
        if (gunData == null)
        {
            gunData = ScriptableObject.CreateInstance<GunData>(); // Tạo instance mới của GunData

        }
        bulletCountText.text = gunData.bulletCount.ToString(); // cập nhật hiển thị sl đạn
        // Gán vị trí của shootingPoint bằng gunPosition
        if (shootingPoint != null)
        {
            // shootingPoint.position = PlayerCustomization.gunPosition;  // Gán vị trí bắn
        }
        // Khởi tạo số lượng đạn còn lại
        _currentBulletCount = gunData.bulletCount;
        // Lấy PlayerController từ GameObject cha hoặc nơi nào đó
        _playerController = GetComponent<PlayerController>();
        UpdatePlayerGunData();
    }
    void Update()
    {

        // Cập nhật vị trí của shootingPoint
        if (shootingPoint != null)
        {
            // Giả sử player là Transform của nhân vật
            Transform playerTransform = transform; // vật (hoặc bạn có thể chỉ địGán transform của nhân nh đối tượng nhân vật cụ thể)
            Vector3 offset = new Vector3(0.9f, -0.1f, 0); // Tạo khoảng cách trước mặt nhân vật
            if (transform.localScale.x < 0)
            {
                offset = new Vector3(-0.9f, -0.1f, 0); // Đổi chiều nếu nhân vật quay trái
            }
            shootingPoint.position = transform.position + offset; // Cập nhật vị trí của shootingPoint
            // Đặt vị trí của shootingPoint ở trước mặt player
            // shootingPoint.position = playerTransform.position + playerTransform.right * 0.5f + Vector3.down * 0.3f; // 1.0f là khoảng cách từ player đến shootingPoint
            // shootingPoint.rotation = playerTransform.rotation; // Đặt hướng của shootingPoint  giống hướng của player
        }



        //if (Input.GetButtonDown("Fire1")) // Kiểm tra nếu người chơi nhấn phím bắn
        //{
        //    Shoot();
        //}

        //if (Input.GetKeyDown(KeyCode.Q)) // Kiểm tra nếu người chơi nhấn phím R để nạp lại đạn
        //{
        //    Reload();
        //}
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
    public void OnShootButtonClicked()
    {
        Debug.Log("Bắn súng!");
        Shoot();
        // Thêm logic bắn súng ở đây
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
                //for (int i = 0; i < gunData.bulletCount; i++)
                //{
                // GameObject bullet = Instantiate(gunData.bulletPrefab, shootingPoint.position, shootingPoint.rotation);
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
                // }

                // Giảm số lượng đạn còn lại
                _currentBulletCount--;

                bulletCountText.text = _currentBulletCount.ToString(); // cập nhật hiển thị sl đạn

                // Kiểm tra nếu hết đạn
                if (_currentBulletCount <= 0)
                {
                    Debug.Log("Hết đạn! Không thể bắn nữa.");
                    Reloading(); // Tự động nạp lại đạn

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
    public void Reloading()
    {
        _currentBulletCount = gunData.bulletCount; // Nạp lại số lượng đạn
        Debug.Log("Đạn đã được nạp");
    }
    // Coroutine để xóa viên đạn sau 1,5 giây
    private IEnumerator DestroyBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet); // Xóa viên đạn
    }
    public void ChangeToRandomGun()
    {
        if (guns.Count > 0)
        {
            int randomIndex = Random.Range(0, guns.Count); // Lấy chỉ số ngẫu nhiên
            ChangeGun(randomIndex); // Thay đổi súng theo chỉ số ngẫu nhiên
            Debug.Log("Đã đổi sang súng ngẫu nhiên: " + gunData.gunName);
        }
        else
        {
            Debug.LogWarning("Danh sách súng rỗng!");
        }
    }
    public void ChangeGun(int newGunIndex)
    {
        
        if (newGunIndex != _currentGunIndex && newGunIndex < guns.Count)
        {
            if (newGunIndex != _currentGunIndex && newGunIndex < guns.Count)
            {
                _currentGunIndex = newGunIndex;
                gunData = guns[_currentGunIndex]; // Cập nhật GunData
                _currentBulletCount = gunData.bulletCount; // Cập nhật số lượng đạn
                UpdatePlayerGunData(); // Cập nhật GunData trong PlayerController
                bulletCountText.text = _currentBulletCount.ToString(); // Cập nhật số lượng đạn trên UI
                Debug.Log("Đã thay đổi sang khẩu súng: " + gunData.gunName);
            }
            else
            {
                Debug.LogWarning("Chỉ số súng không hợp lệ hoặc súng hiện tại đã được trang bị.");
            }
        }
    }
}


