using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAI : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float detectionDistance = 0.5f;
    public LayerMask groundLayer;
    public Transform Raycast1;
    public Transform Raycast2;
    public Transform Raycast3;

    public float jumpForce = 5f;
    private Rigidbody2D rb;

    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(JumpRoutine());
        StartCoroutine(RandomFlipRoutine());
    }

    void Update()
    {
        Move();
        CheckForPit();
    }

    void Move()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

    void CheckForPit()
    {
        Vector2 direction = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(Raycast1.position, direction, 0.2f, groundLayer); // Tăng khoảng cách raycast
        Debug.DrawRay(Raycast1.position, direction * 0.2f, Color.red);

        RaycastHit2D hit2 = Physics2D.Raycast(Raycast2.position, direction, 1f, groundLayer);
        Debug.DrawRay(Raycast2.position, direction * 1f, Color.green);

        // Chỉ khi raycast 2 không phát hiện mặt đất thì mới flip
        if (hit2.collider == null)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Jump();
        }
    }

    void Jump()
    {
        RaycastHit2D hit = Physics2D.Raycast(Raycast3.position, Vector2.down, 0.1f, groundLayer);
        if (hit.collider != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    IEnumerator RandomFlipRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(8f, 15f);
            yield return new WaitForSeconds(waitTime);
            Flip();
        }
    }
}
