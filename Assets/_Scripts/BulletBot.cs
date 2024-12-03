using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBot : MonoBehaviour
{
    public int damage = 10; // Số máu giảm khi đạn trúng bot

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Giả sử bot có tag là "Player"
        {
            // Lấy script của player và giảm máu
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Giảm máu của player
            }

            // Phá hủy đạn sau khi va chạm
            Destroy(gameObject);
        }
        else
        {
            // Nếu đạn va vào vật thể khác (không phải bot), đạn cũng bị phá hủy
            Destroy(gameObject);
        }
        
    }
}
 