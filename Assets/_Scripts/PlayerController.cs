﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerData playerData;      // Tham chiếu tới dữ liệu nhân vật
    public bool isFacingRight = true;  // Kiểm tra hướng của nhân vật
    private Rigidbody2D _rb;           // Rigidbody của nhân vật
    private int _jumpCount = 0;        // Biến đếm số lần nhảy
    // private bool _isGrounded;          // Kiểm tra xem nhân vật có chạm đất không
    private GunController _gunController; // Điều khiển súng
    // private float moveInput = 0f;
    private bool moveLeft = false;
    private bool moveRight = false;
    private Vector2 _initialPosition; // Lưu trữ vị trí ban đầu của nhân vật
    // private bool _isStopped = false; // Kiểm tra trạng thái đứng im

    private void Start()
    {
        // Lấy thành phần Rigidbody2D của nhân vật
        _rb = GetComponent<Rigidbody2D>();
        // Lưu trữ vị trí ban đầu của nhân vật
        _initialPosition = transform.position;
        // Khởi tạo dữ liệu nhân vật với giá trị mặc định
        if (playerData == null)
        {
            playerData = ScriptableObject.CreateInstance<PlayerData>(); // Tạo instance mới của PlayerData
        }
        // In ra tên và máu của nhân vật
        Debug.Log("Nhân vật: " + playerData.characterName + ", Máu: " + playerData.health);



    }


    void Update()
    {
        // Gọi hàm di chuyển
        if (moveLeft)
        {
            transform.Translate(Vector3.left * playerData.moveSpeed * Time.deltaTime);
            Flip();
        }
        if (moveRight)
        {
            transform.Translate(Vector3.right * playerData.moveSpeed * Time.deltaTime);
            Flip();
        }
        // Gọi hàm nhảy
        // Jump();

        // Kiểm tra xem nhân vật có còn máu không
        CheckHealth();

    }
    public void UpdateGunData(GunData newGunData)
    {
        playerData.GunData = newGunData; // Cập nhật GunData trong PlayerData
    }
    // Hàm điều khiển di chuyển nhân vật
    // Hàm điều khiển di chuyển nhân vật
    public void StartMoveLeft()
    {
        moveLeft = true;
    }

    // Hàm dừng di chuyển trái
    public void StopMoveLeft()
    {
        moveLeft = false;
    }

    // Hàm kích hoạt di chuyển phải
    public void StartMoveRight()
    {
        moveRight = true;
    }

    // Hàm dừng di chuyển phải
    public void StopMoveRight()
    {
        moveRight = false;
    }
    // Xử lý sự kiện khi nhấn nút
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "LeftButton")
        {
            StartMoveLeft();
        }
        else if (eventData.pointerPress.name == "RightButton")
        {
            StartMoveRight();
        }
    }

    // Xử lý sự kiện khi thả nút
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "LeftButton")
        {
            StopMoveLeft();
        }
        else if (eventData.pointerPress.name == "RightButton")
        {
            StopMoveRight();
        }
    }




    public void JumpDown()
    {
        RaycastHit2D hitDown;
        float rayDistanceDown = 0.2f; // Khoảng cách ray để kiểm tra collider bên dưới
        Vector2 rayOrigin = transform.position;

        // Dùng raycast để kiểm tra collider ở dưới
        hitDown = Physics2D.Raycast(rayOrigin, Vector2.down, rayDistanceDown);
        if (hitDown.collider != null)
        {
            // Di chuyển player xuống vị trí đáy của collider phía dưới
            transform.position = new Vector2(transform.position.x, hitDown.collider.bounds.min.y - 0.2f);
            Debug.Log("Nhân vật đã nhảy xuống một lớp collider.");
        }
        else
        {
            Debug.Log("Không còn lớp collider ở dưới.");
        }
    }

    public void Jump()
    {
        RaycastHit2D hitUp;
        float rayDistanceUp = 1f; // Khoảng cách ray ngắn hơn để chỉ phát hiện collider gần nhất

        Vector2 rayOrigin = transform.position;

        // Di chuyển lên khi nhấn phím W

        // Dùng raycast để kiểm tra collider ở trên, tính từ vị trí hiện tại
        hitUp = Physics2D.Raycast(rayOrigin, Vector2.up, rayDistanceUp);
        if (hitUp.collider != null)
        {
            // Di chuyển player lên vị trí đỉnh của collider phía trên
            transform.position = new Vector2(transform.position.x, hitUp.collider.bounds.max.y + 0.7f);
            Debug.Log("Di chuyển lên trên một lớp collider.");
        }
        else
        {
            // Không tìm thấy collider, không thể di chuyển lên
            Debug.Log("Không còn lớp collider ở trên.");
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
        // Kiểm tra va chạm với đối tượng có tag "Wall"
        if (collision.gameObject.CompareTag("wall"))
        {
            transform.position = _initialPosition; // Di chuyển về vị trí ban đầu
            Debug.Log("Nhân vật đã va chạm với tường và quay về vị trí ban đầu.");
        }

    }
}
