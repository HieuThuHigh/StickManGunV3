using UnityEngine;
using UnityEngine.UI;

public class AlHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int botHealth;
    public int lives = 3;
    public GameObject al;

    public Image healthBar;
    public Text livesText;

    private GameObject winGamePanel;
    private float botTargetHealth;
    private float lerpSpeed = 5f;

    private GameManager gameManager; // Thêm một tham chiếu đến GameManager

    void Start()
    {
        // Tìm GameManager trong cảnh
        gameManager = FindObjectOfType<GameManager>();

        // Tìm WinGamePanel bằng tag
        winGamePanel = GameObject.FindGameObjectWithTag("Win");

        if (winGamePanel != null)
        {
            winGamePanel.SetActive(false);
            Debug.Log("WinGamePanel đã được tìm thấy và ẩn đi.");
        }
        else
        {
            Debug.LogWarning("Không tìm thấy WinGamePanel với tag WinGamePanel!");
        }

        botHealth = maxHealth;
        botTargetHealth = botHealth;
        UpdateHealthBar();
        UpdateLivesText();
    }

    void Update()
    {
        botHealth = (int)Mathf.Lerp(botHealth, botTargetHealth, lerpSpeed * Time.deltaTime);
        UpdateHealthBar();
    }

    public void BotDamage(int damage)
    {
        botTargetHealth -= damage;

        if (botTargetHealth <= 0)
        {
            lives--;
            UpdateLivesText();

            if (lives > 0)
            {
                botTargetHealth = maxHealth;
                botHealth = maxHealth;
                UpdateHealthBar();
            }
            else
            {
                al.SetActive(false);
                CheckAllBotsDefeated();
            }
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar)
        {
            healthBar.fillAmount = (float)botHealth / maxHealth;

        }
    }

    void UpdateLivesText()
    {
        livesText.text = "x " + lives;
    }

    void CheckAllBotsDefeated()
    {
        // Đếm số lượng bot còn lại trong cảnh
        GameObject[] remainingBots = GameObject.FindGameObjectsWithTag("Bot");

        if (remainingBots.Length == 0)
        {
            gameManager.TriggerWinGame();  // Gọi phương thức từ GameManager khi tất cả bot bị tiêu diệt
        }
    }
}
