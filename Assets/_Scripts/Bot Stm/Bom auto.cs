using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomauto : MonoBehaviour
{
    public float explosionRadius = 1f;    // Bán kính vụ nổ
    public float explosionForce = 50f;   // Lực nổ
    public float initialForce = 3f;     // Lực ban đầu
    public float delay = 3f;              // Thời gian chờ trước khi nổ
    public GameObject explosionEffect;    // Hiệu ứng vụ nổ

    public Rigidbody2D Rb;

    private float countdown;
    private bool hasExploded = false;
    private bool hasTouchedGround = false; // Cờ đánh dấu bom đã chạm đất

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
            if (Rb != null)
    {
        Rb.AddForce(Vector2.up * initialForce); // Thay đổi Vector2.up thành hướng bạn muốn
    }
    }

    // Update is called once per frame
    void Update()
    {
        // Nếu bom đã chạm đất thì bắt đầu đếm ngược
        if (hasTouchedGround && !hasExploded)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f)
            {
                Explode();
                hasExploded = true;
            }
        }
    }


    void Explode()
    {
        // Hiển thị hiệu ứng nổ
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);

        // Hủy hiệu ứng nổ sau 1 giây
        Destroy(explosion, 1f);

        // Tìm tất cả các đối tượng trong bán kính vụ nổ
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D nearbyObject in colliders)
        {
            // Thêm lực vào các đối tượng có Rigidbody2D
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce((rb.transform.position - transform.position).normalized * explosionForce);
            }
            PlayerHealth playerHealth = nearbyObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(50); // Trừ 50 máu của Player
            }

            // Gây sát thương cho các đối tượng có thể phá hủy (nếu có)
            // Ví dụ: Health health = nearbyObject.GetComponent<Health>();
            // if (health != null) { health.TakeDamage(50); }
        }
       

        // Hủy bom sau khi nổ
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (!hasExploded && !hasTouchedGround)
        {
            hasTouchedGround = true; // Đánh dấu là bom đã chạm đất
            countdown = delay;       // Bắt đầu đếm ngược
        }
    }
}


