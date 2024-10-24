using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonoBehaviour
{
    public Transform player; // Tham chiếu đến Transform của người chơi
    public float speed = 2f; // Tốc độ di chuyển
    public float jumpForce = 5f; // Lực nhảy
    public float jumpHeightThreshold = 1.5f; // Độ cao tối đa để enemy nhảy
    public float jumpCooldown = 1f; // Thời gian giữa các lần nhảy
    public float stopDistance = 2f; // Khoảng cách dừng lại khi gần player
    private Rigidbody2D rb;

    private float jumpTimer; // Bộ đếm thời gian để kiểm soát thời gian hồi nhảy

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FollowPlayer();
        jumpTimer -= Time.deltaTime; // Giảm bộ đếm thời gian nhảy
    }

    void FollowPlayer()
    {
        RaycastHit2D hitDown;
        Vector2 rayOrigin = transform.position;
        float rayDistanceDown = 0.2f;
        if (player != null)
        {
            // Tính khoảng cách giữa enemy và player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            hitDown = Physics2D.Raycast(rayOrigin, Vector2.down, rayDistanceDown);
            // Nếu khoảng cách lớn hơn stopDistance, enemy sẽ di chuyển về phía player
            if (distanceToPlayer > stopDistance)
            {
                // Di chuyển enemy theo hướng của người chơi
                Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                // Kiểm tra điều kiện nhảy
                if (player.position.y > transform.position.y && 
                    (player.position.y - transform.position.y) < jumpHeightThreshold && 
                    jumpTimer <= 0)
                {
                    Jump();
                }
                if (player.position.y < transform.position.y &&
                    jumpTimer <= 0&& hitDown.collider != null)
                {
                    
                    transform.position = new Vector2(transform.position.x, hitDown.collider.bounds.min.y + 0.3f);
                }
            }
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        jumpTimer = jumpCooldown; // Đặt lại bộ đếm thời gian nhảy
    }

}
