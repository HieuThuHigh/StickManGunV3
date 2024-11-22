using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBox : MonoBehaviour
{
    void Start()
    {
         Destroy(gameObject, 10f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Lấy GunController từ đối tượng người chơi
            GunController gunController = other.GetComponent<GunController>();
            if (gunController != null)
            {
                gunController.ChangeToRandomGun(); // Gọi hàm để đổi súng ngẫu nhiên
                Destroy(gameObject); // Xóa hộp sau khi đổi súng
                
            }
        }
    }
}
