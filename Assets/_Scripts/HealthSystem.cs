//using UnityEngine;
//using UnityEngine.UI;

//public class HealthSystem : MonoBehaviour
//{
//    public int numberOfHearts = 3; // Số trái tim ban đầu
//    public int luongMauHienTai;
//    public int  health = 100;
//    public Text heartsText; // Text để hiển thị số lượng trái tim 
//    public thanhMau thanhmau; // Thanh máu
//    private PlayerData playerData;
    

//    void Start()
//    {
        
//        luongMauHienTai =  health;
//        thanhmau.capNhatThanhMau(luongMauHienTai,  health);
//        UpdateHeartsUI(); // Cập nhật giao diện số lượng trái tim
//    }

//    void Update()
//    {
        
//        // Nếu máu cạn và vẫn còn trái tim
//        if (luongMauHienTai <= 0 && numberOfHearts > 0)
//        {
//            LoseHeart();
//        }

//        thanhmau.capNhatThanhMau(luongMauHienTai,  health); // Cập nhật thanh máu
//        UpdateHeartsUI(); // Cập nhật giao diện trái tim (text)
//    }

//    void LoseHeart()
//    {
//        numberOfHearts--; // Mất một trái tim
//        if (numberOfHearts > 0)
//        {
//            luongMauHienTai =  health; // Hồi lại máu đầy cho trái tim tiếp theo
//        }
//        else
//        {
//            Die(); // Nếu không còn trái tim nào, nhân vật chết
//        }
//    }

//    void UpdateHeartsUI()
//    {
//        // Cập nhật số lượng trái tim trong Text
//        heartsText.text = "x " + numberOfHearts.ToString(); // Ví dụ: x 3
//    }

//    void Die()
//    {
//        // Xử lý khi nhân vật chết
//        Destroy(this.gameObject);
//        Debug.Log("Nhân vật đã chết!");
//        // Thêm logic khi game over ở đây nếu cần
//    }

//    // Hàm gọi khi nhân vật bị mất máu
//    public void TakeDamage(int damage)
//    {
//        luongMauHienTai -= damage;
//        if (luongMauHienTai < 0)
//        {f
//            luongMauHienTai = 0;
//        }
//    }
//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.tag == "bom")
//        {
//            TakeDamage(50);
//        }
//    }
//}
