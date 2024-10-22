using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;      // Tham chiếu tới dữ liệu nhân vật
    public bool isFacingRight = true;  // Kiểm tra hướng của nhân vật
    private Rigidbody2D _rb;           // Rigidbody của nhân vật
    private int _jumpCount = 0;        // Biến đếm số lần nhảy
    private bool _isGrounded;          // Kiểm tra xem nhân vật có chạm đất không
    private GunController _gunController; // Điều khiển súng

    private void Start()
    {
        // Lấy thành phần Rigidbody2D của nhân vật
        _rb = GetComponent<Rigidbody2D>();

        // Khởi tạo dữ liệu nhân vật với giá trị mặc định
        if (playerData == null)
        {
            playerData = ScriptableObject.CreateInstance<PlayerData>(); // Tạo instance mới của PlayerData
        }
        // In ra tên và máu của nhân vật
        Debug.Log("Nhân vật: " + playerData.characterName + ", Máu: " + playerData.health);
    }
   

    private void Update()
    {
        // Gọi hàm di chuyển
        Move();

        // Gọi hàm nhảy
        Jump();

        // Kiểm tra xem nhân vật có còn máu không
        CheckHealth();
    }
    public void UpdateGunData(GunData newGunData)
    {
        playerData.GunData = newGunData; // Cập nhật GunData trong PlayerData
    }
    // Hàm điều khiển di chuyển nhân vật
    void Move()
    {
        float moveInput = 0f;

        // Kiểm tra phím A và D
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("suyedgfuyisefsf");
            Flip();
            moveInput = -1f; // Di chuyển sang trái
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveInput = 1f; // Di chuyển sang phải
        }

        // Cập nhật vị trí của nhân vật dựa trên moveSpeed từ PlayerData
        transform.position += new Vector3(moveInput * playerData.moveSpeed * Time.deltaTime, 0f, 0f);
    }

    // Hàm điều khiển nhân vật nhảy
    //void Jump()
    //{
    //    // Nếu nhấn phím nhảy và số lần nhảy nhỏ hơn số lần nhảy tối đa
    //    if (Input.GetKeyDown(KeyCode.W) && _jumpCount < playerData.maxJumps)
    //    {
    //        // Áp dụng lực nhảy theo trục Y dựa trên jumpForce từ PlayerData
    //        _rb.velocity = new Vector2(_rb.velocity.x, playerData.jumpForce);
    //        _jumpCount++;  // Tăng số lần nhảy lên
    //        Debug.Log("Nhân vật đã nhảy! Số lần nhảy hiện tại: " + _jumpCount);
    //    }
    //}

    void Jump()
    {
        RaycastHit2D hitUp, hitDown;
        float rayDistanceUp = 1f; // Khoảng cách ray ngắn hơn để chỉ phát hiện collider gần nhất
        float rayDistanceDown = 0.2f;
        Vector2 rayOrigin = transform.position;

        // Di chuyển lên khi nhấn phím W
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Dùng raycast để kiểm tra collider ở trên, tính từ vị trí hiện tại
            hitUp = Physics2D.Raycast(rayOrigin, Vector2.up, rayDistanceUp);
            if (hitUp.collider != null)
            {
                // Di chuyển player lên vị trí đỉnh của collider phía trên
                transform.position = new Vector2(transform.position.x, hitUp.collider.bounds.max.y + 0.5f);
                Debug.Log("Di chuyển lên trên một lớp collider.");
            }
            else
            {
                // Không tìm thấy collider, không thể di chuyển lên
                Debug.Log("Không còn lớp collider ở trên.");
            }
        }

        // Di chuyển xuống khi nhấn phím S
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Dùng raycast để kiểm tra collider ở dưới, tính từ vị trí hiện tại
            hitDown = Physics2D.Raycast(rayOrigin, Vector2.down, rayDistanceDown);
            if (hitDown.collider != null)
            {
                // Di chuyển player xuống vị trí đáy của collider phía dưới
                transform.position = new Vector2(transform.position.x, hitDown.collider.bounds.min.y - 0.2f);
                Debug.Log("Di chuyển xuống dưới một lớp collider.");
            }
            else
            {
                // Không tìm thấy collider, không thể di chuyển xuống
                Debug.Log("Không còn lớp collider ở dưới.");
            }
        }
    }







    // Hàm kiểm tra máu của nhân vật
    void CheckHealth()
    {
        if (playerData.health <= 0)
        {
            // Xử lý khi nhân vật hết máu (có thể là Game Over hoặc làm gì đó)
            Debug.Log("Nhân vật đã hết máu!");
        }
    }

    // Hàm lật nhân vật khi di chuyển trái/phải
    void Flip()
    {
        isFacingRight = !isFacingRight; // Đảo hướng của nhân vật
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;                 // Lật nhân vật bằng cách đảo trục X
        transform.localScale = scaler;
    }

    // Hàm để đặt hướng của shootingPoint
    private void SetShootingPointDirection(float direction)
    {
        if (_gunController.shootingPoint != null)
        {
            // Đặt điểm bắn theo hướng
            _gunController.shootingPoint.localPosition = new Vector3(direction * 0.5f, 0f, 0f); // Thay đổi vị trí shootingPoint
            _gunController.shootingPoint.rotation = Quaternion.LookRotation(Vector3.forward, direction == -1f ? Vector3.down : Vector3.up); // Hướng bắn
        }
    }

    // Hàm xử lý khi nhân vật chạm vào mặt đất (reset số lần nhảy)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem nhân vật có chạm vào đối tượng có tag "Ground" không
        if (collision.gameObject.CompareTag("Ground"))
        {
            _jumpCount = 0;  // Reset số lần nhảy về 0 khi chạm đất
            Debug.Log("Nhân vật đã chạm đất, reset số lần nhảy.");
        }
    }
}
