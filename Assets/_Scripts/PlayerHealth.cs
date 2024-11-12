using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;             // Máu tối đa của Player
    public int currentHealth;               // Máu hiện tại của Player
    public int lives = 3;                   // Số mạng của Player
    public GameObject gameOverPanel;        // Panel Game Over
    public GameObject winGamePanel;
    public GameObject player;               // Tham chiếu đến Player
    

    public Image healthBar;                 // Image thanh máu trên Canvas
    public Text livesText;                  // Text hiển thị số lượng mạng bên cạnh icon trái tim

    private float targetHealth;             // Giá trị thanh máu mục tiêu
    private float lerpSpeed = 5f;           // Tốc độ thay đổi thanh máu

    void Start()
    {
        currentHealth = maxHealth;          // Khởi tạo máu của Player
        targetHealth = currentHealth;       // Thiết lập thanh máu mục tiêu
        UpdateHealthBar();                  // Cập nhật thanh máu lúc đầu
        UpdateLivesText();                  // Cập nhật Text hiển thị số mạng lúc đầu
        gameOverPanel.SetActive(false);     // Ẩn Game Over Panel lúc đầu
       
    }

    void Update()
    {
        // Cập nhật thanh máu mượt mà
        currentHealth = (int)Mathf.Lerp(currentHealth, targetHealth, lerpSpeed * Time.deltaTime);
        UpdateHealthBar();                  // Cập nhật thanh máu sau khi thay đổi

        // Kiểm tra nếu player bị phá hủy hoặc bị ẩn
        if (player == null)
        {
            if (!gameOverPanel.activeSelf)
            {
                gameOverPanel.SetActive(true); // Hiển thị Game Over Panel
                Time.timeScale = 0;             // Dừng thời gian (game stop)
            }
        }
        
    }

    public void TakeDamage(int damage)
    {
        // Giảm máu khi nhận thiệt hại
        targetHealth -= damage;

        // Nếu máu <= 0, trừ 1 mạng
        if (targetHealth <= 0)
        {
            lives--;                        // Trừ 1 mạng khi máu <= 0
            UpdateLivesText();              // Cập nhật lại số mạng

            if (lives > 0)
            {
                targetHealth = maxHealth;   // Đặt lại thanh máu mục tiêu
                currentHealth = maxHealth;  // Hồi đầy máu ngay lập tức
                UpdateHealthBar();   // Hồi lại máu đầy khi Player vẫn còn mạng
            }
            else
            {
                GameOver();        // Hủy Player khi hết mạng
            }
        }
        // Không làm gì nếu player không chết, chỉ giảm máu
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;  // Cập nhật thanh máu
    }

    void UpdateLivesText()
    {
        livesText.text = "x " + lives;      // Cập nhật Text hiển thị số mạng
    }

    void GameOver()
    {
        // Ẩn Player thay vì hủy, tránh lỗi tham chiếu null
        player.SetActive(false);
        // Hiển thị Game Over Panel
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
  

    public void thoat()
    {
        SceneManager.LoadScene("SPL");
    }

    public void Reload()
    {
        gameOverPanel.SetActive(false);      // Đảm bảo tắt Game Over Panel
        Time.timeScale = 1;                  // Khởi động lại thời gian
        currentHealth = maxHealth;
        targetHealth = currentHealth;  // Đặt lại targetHealth để tránh hồi máu không mong muốn
        lives = 3;
        UpdateHealthBar();
        UpdateLivesText();
        player.SetActive(true);              // Đảm bảo Player xuất hiện lại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
