using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIeasy : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 5f;
    public float jumpHeightThreshold = 1.5f;
    public float jumpCooldown = 1f;
    public float stopDistance = 2f;
    public LayerMask groundLayer;
    public Transform Raycast1;
    public Transform Raycast2;
    public Transform Raycast3;
    private Rigidbody2D rb;

    private float jumpTimer;
    private float stopTimer = 0f;
    [SerializeField] private float fallDelay = 1f;
    private float fallDelayTimer = 0f;

    private Transform player; // Tham chiếu tới player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Tìm player theo tên GameObject
        player = GameObject.FindWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Please ensure the player GameObject is named 'Player'.");
        }
    }

    void Update()
    {
        jumpTimer -= Time.deltaTime;
        stopTimer -= Time.deltaTime;
        fallDelayTimer -= Time.deltaTime;

        if (stopTimer > 0)
        {
            return;
        }

        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (player == null) return;

        RaycastHit2D hitDown;
        Vector2 rayOrigin = transform.position;
        float rayDistanceDown = 0.2f;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        hitDown = Physics2D.Raycast(rayOrigin, Vector2.down, rayDistanceDown);
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(Raycast1.position, direction, 0.1f, groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(Raycast2.position, direction, 2f, groundLayer);
        RaycastHit2D hit3 = Physics2D.Raycast(Raycast3.position, direction, 1f, groundLayer);
        Debug.DrawRay(Raycast1.position, direction * 0.1f, Color.red);
        Debug.DrawRay(Raycast2.position, direction * 1f, Color.green);
        Debug.DrawRay(Raycast3.position, direction * 1f, Color.blue);

        if (hit.collider == null && hit2.collider == null)
        {
            stopTimer = 1f;
            Flip();
            return;
        }

        if (distanceToPlayer > stopDistance)
        {
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (targetPosition.x > transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (targetPosition.x < transform.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            if (player.position.y > transform.position.y && ((player.position.y - transform.position.y) < jumpHeightThreshold) && jumpTimer <= 0)
            {
                Jump();
            }

            if (player.position.y < transform.position.y && jumpTimer <= 0 && hitDown.collider != null && hit3.collider != null)
            {
                if (fallDelayTimer <= 0)
                {
                    transform.position = new Vector2(transform.position.x, hitDown.collider.bounds.min.y + 0.2f);
                    fallDelayTimer = fallDelay;
                }
            }
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        jumpTimer = jumpCooldown;
    }

    void Flip()
    {
        if (player != null)
        {
            if (player.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
