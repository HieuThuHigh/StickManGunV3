using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;

    public void SetDamage(int value)
    {
        damage = value;
    }

    // Xử lý va chạm hoặc sát thương tại đây
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra và xử lý sát thương
        // Ví dụ: collision.GetComponent<Health>().TakeDamage(damage);
    }
}
