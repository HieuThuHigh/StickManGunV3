using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _moveSpeed = 5f;           // Tốc độ di chuyển
    private float _jumpForce = 8f;           // Lực nhảy
    public bool isFacingRight = true;        // Kiểm tra hướng của nhân vật
    private Rigidbody2D _rb;                 // Rigidbody của nhân vật

    private int _jumpCount = 0;              // Biến đếm số lần nhảy
    private int _maxJumps = 1;               // Số lần nhảy tối đa (2 lần)
    private bool _isGrounded;                // Kiểm tra xem nhân vật có chạm đất không

    private void Start()
    {
        // Lấy thành phần Rigidbody2D của nhân vật
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Gọi hàm di chuyển
        Move();

        // Gọi hàm nhảy
        Jump();
    }

    // Hàm điều khiển di chuyển nhân vật
    void Move()
    {
        // Nhận đầu vào từ bàn phím (trái/phải)
        float moveInput = Input.GetAxis("Horizontal");
        Debug.Log("Giá trị moveInput: " + moveInput); // In giá trị ra console

        // Di chuyển nhân vật theo trục x dựa trên đầu vào
        _rb.velocity = new Vector2(moveInput * _moveSpeed, _rb.velocity.y);

        // Kiểm tra hướng di chuyển và lật nhân vật nếu cần
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
            Debug.Log("Nhân vật di chuyển sang phải!");
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
            Debug.Log("Nhân vật di chuyển sang trái!");
        }
    }

    // Hàm điều khiển nhân vật nhảy
    void Jump()
    {
        // Nếu nhấn phím nhảy và số lần nhảy nhỏ hơn số lần nhảy tối đa
        if (Input.GetKeyDown(KeyCode.W) && _jumpCount < _maxJumps)
        {
            // Áp dụng lực nhảy theo trục Y
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _jumpCount++;  // Tăng số lần nhảy lên
            Debug.Log("Nhân vật đã nhảy! Số lần nhảy hiện tại: " + _jumpCount);
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
