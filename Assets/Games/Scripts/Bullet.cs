using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float lifetime = 1f; // Thời gian tồn tại của đạn

    private void Start()
    {
        // Hủy đạn sau một khoảng thời gian
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm với đối tượng khác
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Ghi điểm hoặc xử lý logic tiêu diệt
            Destroy(collision.gameObject); // Hủy enemy
            Destroy(gameObject); // Hủy đạn
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            // Hủy đạn khi chạm đất
            Destroy(gameObject);
        }
    }
}


