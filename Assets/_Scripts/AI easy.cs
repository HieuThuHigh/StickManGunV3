using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIeasy : MonoBehaviour
{
    public Transform player; // Tham chiếu đến Transform của người chơi
    public float speed = 2f; // Tốc độ di chuyển
    public float jumpForce = 5f; // Lực nhảy
    public float jumpHeightThreshold = 1.5f; // Độ cao tối đa để enemy nhảy
    public float jumpCooldown = 1f; // Thời gian giữa các lần nhảy
    public float stopDistance = 2f; // Khoảng cách dừng lại khi gần player
    public LayerMask groundLayer;
    public Transform Raycast1;
    public Transform Raycast2;
    public Transform Raycast3;
    private Rigidbody2D rb;

    private float jumpTimer; // Bộ đếm thời gian để kiểm soát thời gian hồi nhảy
    private float stopTimer = 0f; // Bộ đếm thời gian dừng lại khi không tìm thấy mặt đất
    [SerializeField] private float fallDelay = 1f; // Thời gian trễ trước khi tụt xuống dưới, có thể chỉnh sửa từ Inspector
    private float fallDelayTimer = 0f; // Bộ đếm thời gian trễ

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        jumpTimer -= Time.deltaTime; // Giảm bộ đếm thời gian nhảy
        stopTimer -= Time.deltaTime; // Giảm bộ đếm thời gian dừng lại
        fallDelayTimer -= Time.deltaTime; // Giảm bộ đếm thời gian trễ

        // Nếu stopTimer > 0, có nghĩa là AI đang tạm dừng, không di chuyển
        if (stopTimer > 0)
        {
            return; // Ngừng di chuyển trong thời gian dừng lại
        }

        // Nếu stopTimer <= 0, tiếp tục di chuyển AI theo player
        FollowPlayer();
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
            Vector2 direction = Vector2.down;

            // Kiểm tra hai raycast có chạm vào mặt đất không
            RaycastHit2D hit = Physics2D.Raycast(Raycast1.position, direction, 0.1f, groundLayer);
            RaycastHit2D hit2 = Physics2D.Raycast(Raycast2.position, direction, 2f, groundLayer);
            RaycastHit2D hit3 = Physics2D.Raycast(Raycast3.position, direction, 1f, groundLayer);
            Debug.DrawRay(Raycast1.position, direction * 0.1f, Color.red);
            Debug.DrawRay(Raycast2.position, direction * 1f, Color.green);
            Debug.DrawRay(Raycast3.position, direction * 1f, Color.blue);

            // Nếu cả hai raycast không chạm vào mặt đất, dừng AI lại một giây
            if (hit.collider == null && hit2.collider == null)
            {
                stopTimer = 1f; // Dừng AI 1 giây
                // Quay mặt AI về hướng ngược lại sau khi dừng lại
                Flip();
                return; // Ngừng di chuyển trong thời gian dừng
            }

            // Nếu khoảng cách lớn hơn stopDistance, enemy sẽ di chuyển về phía player
            if (distanceToPlayer > stopDistance)
            {
                // Di chuyển enemy theo hướng của người chơi
                Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                // Quay mặt AI theo hướng di chuyển
                if (targetPosition.x > transform.position.x)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Quay mặt sang phải
                }
                else if (targetPosition.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Quay mặt sang trái
                }

                // Kiểm tra điều kiện nhảy
                if (player.position.y > transform.position.y && (
                    (player.position.y - transform.position.y) < jumpHeightThreshold) &&
                    jumpTimer <= 0)
                {
                    Jump();
                }

                // Kiểm tra điều kiện tụt xuống dưới với trễ
                if (player.position.y < transform.position.y &&
                    jumpTimer <= 0 && hitDown.collider != null && hit3.collider != null)
                {
                    // Nếu thời gian trễ đã đủ, tụt xuống dưới
                    if (fallDelayTimer <= 0)
                    {
                        transform.position = new Vector2(transform.position.x, hitDown.collider.bounds.min.y + 0.2f);
                        fallDelayTimer = fallDelay; // Đặt lại thời gian trễ theo giá trị fallDelay từ Inspector
                    }
                }
            }
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        jumpTimer = jumpCooldown; // Đặt lại bộ đếm thời gian nhảy
    }

    // Hàm Flip để quay mặt AI ngược lại
    void Flip()
    {
        // Quay mặt AI về hướng ngược lại
        if (player != null)
        {
            // Nếu player nằm phía bên phải AI
            if (player.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Quay mặt sang phải
            }
            // Nếu player nằm phía bên trái AI
            else if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Quay mặt sang trái
            }
        }
    }
}
