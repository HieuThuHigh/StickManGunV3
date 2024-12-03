using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // Số máu giảm khi đạn trúng bot

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem va chạm có phải là với bot không
        if (collision.gameObject.CompareTag("Bot")) // Giả sử bot có tag là "Bot"
        {
            // Lấy script của bot và giảm máu
            AlHealth botHealth = collision.gameObject.GetComponent<AlHealth>();
            if (botHealth != null)
            {
                botHealth.BotDamage(damage); // Giảm máu của bot
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
