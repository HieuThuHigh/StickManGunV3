using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _moveSpeed = 8f;           // Tốc độ di chuyển
    private float _jumpForce = 8f;          // Lực nhảy
    public Transform groundCheck;          // Điểm để kiểm tra xem nhân vật có đứng trên mặt đất không
    public LayerMask groundLayer;          // Lớp mặt đất để kiểm tra va chạm
    private bool isGrounded;               // Kiểm tra xem nhân vật có đang đứng trên mặt đất không
    private bool isFacingRight = true;     // Kiểm tra hướng của nhân vật
    private Rigidbody2D rb;                // Rigidbody của nhân vật

    private void Start()
    {
        // Lấy thành phần Rigidbody2D của nhân vật
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Gọi hàm di chuyển
        Move();

        // Gọi hàm nhảy
        Jump();
        

        // Kiểm tra xem nhân vật có đứng trên mặt đất không
       isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f  , groundLayer);
    }

    // Hàm điều khiển di chuyển nhân vật
    void Move()
    {
        // Nhận đầu vào từ bàn phím (trái/phải)
        float moveInput = Input.GetAxis("Horizontal");
        Debug.Log("Giá trị moveInput: " + moveInput); // In giá trị ra console

        // Di chuyển nhân vật theo trục x dựa trên đầu vào
        rb.velocity = new Vector2(moveInput * _moveSpeed, rb.velocity.y);

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
    // Hàm điều khiển nhân vật nhảy
    void Jump()
    {
        // Nếu nhấn phím nhảy và đang trên mặt đất
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            // Áp dụng lực nhảy theo trục Y
            rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
            Debug.Log("Nhân vật đã nhảy!"); // Thông báo khi nhảy
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
}
