using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private MapData mapData;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject botPrefab;
    [SerializeField] private int numberOfBots = 2;

    [SerializeField] public float gameDuration = 100f;
    private float timeRemaining;
    private bool gameActive = false;
    [SerializeField] private Text timeText;
    [SerializeField] private Button restartButton;

    private GameObject winGamePanel;  // Thêm tham chiếu tới WinGamePanel

    void Start()
    {
        LoadMap();
        LoadPlayer();
        SpawnBots();
        timeRemaining = gameDuration;
        gameActive = true;

        // Tìm WinGamePanel trong Start
        winGamePanel = GameObject.FindGameObjectWithTag("Win");
        if (winGamePanel != null)
        {
            winGamePanel.SetActive(false); // Đảm bảo WinGamePanel không hiển thị khi bắt đầu
        }

        restartButton.gameObject.SetActive(false); // Ẩn nút restart ban đầu
    }

    void Update()
    {
        if (gameActive)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                EndGame();
            }
            else
            {
                UpdateTimeText();
            }
        }
    }
    

    void UpdateTimeText()
    {
        int minutes = ((int)timeRemaining / 60);
        int seconds = ((int)timeRemaining % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void LoadMap()
    {
        if (mapData != null && mapData.mapPrefab != null)
        {
            Vector3 mapPosition = new Vector3(0, 0, 0);
            GameObject mapInstance = Instantiate(mapData.mapPrefab, mapPosition, Quaternion.identity);

            if (mapInstance.GetComponent<Collider>() == null)
            {
                Debug.LogError("Map prefab does not have a collider!");
            }
        }
    }

    public void SpawnBots()
    {
        for (int i = 0; i < numberOfBots; i++)
        {
            Vector3 botPosition = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
            Instantiate(botPrefab, botPosition, Quaternion.identity);
        }
    }

    public void LoadPlayer()
    {
        if (playerPrefab != null)
        {
             PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(Random.Range(-3,3), 1, 0), Quaternion.identity);
            //  Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    private void EndGame()
    {
        gameActive = false;
        Debug.Log("Trò chơi kết thúc!");
        restartButton.gameObject.SetActive(true); // Hiển thị nút Restart
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Tải lại scene hiện tại
    }

    // Phương thức này được gọi khi tất cả bot bị tiêu diệt
    public void TriggerWinGame()
    {
        if (winGamePanel != null)
        {
            winGamePanel.SetActive(true);
            Debug.Log("WinGamePanel đã được hiển thị!");
            Time.timeScale = 0;  // Dừng trò chơi khi thắng
        }
        else
        {
            Debug.LogWarning("Không tìm thấy WinGamePanel!");
        }
    }
}
