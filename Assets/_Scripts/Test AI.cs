using System.Collections;
using UnityEngine;

public class StickmanCombatAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float jumpForce = 8f;
    public float detectionRange = 10f;
    public float stopDistance = 1.5f;
    public float jumpCooldown = 1f;
    public float dropDownCooldown = 1f;
    public float randomJumpIntervalMin = 2f; // Thời gian tối thiểu giữa các lần nhảy
    public float randomJumpIntervalMax = 5f; // Thời gian tối đa giữa các lần nhảy
    public LayerMask groundLayer;
    public LayerMask platformLayer;

    private Rigidbody2D rb;
    private Collider2D botCollider;
    private float jumpTimer;
    private float dropDownTimer;
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        botCollider = GetComponent<Collider2D>();
        jumpTimer = 0f;
        dropDownTimer = 0f;

        // Bắt đầu Coroutine để nhảy ngẫu nhiên
        StartCoroutine(RandomJumpCoroutine());
    }

    void Update()
    {
        // Giảm thời gian chờ cho nhảy và rơi xuống
        jumpTimer -= Time.deltaTime;
        dropDownTimer -= Time.deltaTime;

        if (PlayerInSight())
        {
            FollowPlayer();
        }
        else
        {
            Patrol();
        }

        AdjustCollisionLayer();
    }

    void AdjustCollisionLayer()
    {
        if (Mathf.Abs(rb.velocity.x) < 1f && Mathf.Abs(rb.velocity.y) < 1f)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    bool PlayerInSight()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRange;
    }

    void FollowPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            MoveTowards(player.position.x);
        }

        if (player.position.y > transform.position.y + 0.5f && jumpTimer <= 0)
        {
            Jump();
        }

        if (player.position.y < transform.position.y - 0.5f && dropDownTimer <= 0 && IsPlatformBelow())
        {
            DropDown();
        }

        FlipTowardsPlayer();
    }

    void MoveTowards(float targetX)
    {
        Vector2 targetPosition = new Vector2(targetX, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            StartCoroutine(EnablePlatformCollision(false));
            jumpTimer = jumpCooldown;
        }
    }

    void DropDown()
    {
        if (IsPlatformBelow())
        {
            StartCoroutine(EnablePlatformCollision(false));
            dropDownTimer = dropDownCooldown;
        }
    }

    bool IsPlatformBelow()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, platformLayer);
        return hit.collider != null;
    }

    IEnumerator EnablePlatformCollision(bool enable)
    {
        if (!enable)
        {
            botCollider.enabled = false;
            yield return new WaitForSeconds(0.2f);
            botCollider.enabled = true;
        }
    }

    void Patrol()
    {
        rb.velocity = new Vector2((isFacingRight ? 1 : -1) * moveSpeed, rb.velocity.y);
        if (IsNearEdge())
        {
            FlipDirection();
        }
    }

    void FlipTowardsPlayer()
    {
        if (player.position.x > transform.position.x && !isFacingRight)
        {
            FlipDirection();
        }
        else if (player.position.x < transform.position.x && isFacingRight)
        {
            FlipDirection();
        }
    }

    void FlipDirection()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        return hit.collider != null;
    }

    bool IsNearEdge()
    {
        Vector2 origin = new Vector2(transform.position.x + (isFacingRight ? 0.5f : -0.5f), transform.position.y);
        RaycastHit2D groundInfo = Physics2D.Raycast(origin, Vector2.down, 1f, groundLayer);
        return groundInfo.collider == null;
    }

    // Coroutine to make bot jump randomly
    IEnumerator RandomJumpCoroutine()
    {
        while (true)
        {
            float waitTime = Random.Range(randomJumpIntervalMin, randomJumpIntervalMax);
            yield return new WaitForSeconds(waitTime);

            if (IsGrounded() && jumpTimer <= 0)
            {
                Jump();
            }
        }
    }
}

