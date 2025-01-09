using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData; // Biến để tham chiếu đến PlayerData
    [SerializeField] private MapData mapData;       // Biến để tham chiếu đến MapData
    [SerializeField] private GameObject playerPrefab; // Biến để tham chiếu đến prefab player
    [SerializeField] private GameObject botPrefab; // Tham chiếu đến prefab bot
    [SerializeField] private int numberOfBots = 2; // Số lượng bot muốn thêm


    [SerializeField] private float gameDuration = 100f; // Thời gian cho mỗi trận đấu
    private float timeRemaining; // Thời gian còn lại
    private bool gameActive = false; // Trạng thái trò chơi
    [SerializeField] private Text timeText; // Tham chiếu đến Text hiển thị thời gian
    [SerializeField] private Button restartButton; // Tham chiếu đến nút Restart
    private void Start()
    {
        LoadMap(); // Gọi LoadMap trong Start
        LoadPlayer(); // Tải nhân vật vào scene
        SpawnBots();    // Gọi hàm tạo bot
      //  LoadButton();
        timeRemaining = gameDuration; // Khởi tạo thời gian còn lại
        gameActive = true; // Bắt đầu trò chơi
                           // Thiết lập sự kiện cho nút Restart
      //  restartButton.onClick.AddListener(RestartGame);
       // restartButton.gameObject.SetActive(false); // Ẩn nút Restart ban đầu
    }
    private void Update()
    {
        if (gameActive)
        {
            timeRemaining -= Time.deltaTime; // Giảm thời gian còn lại
            if (timeRemaining <=gameDuration)
            {
                EndGame(); // Kết thúc trò chơi khi hết thời gian
            }
        }
    }
    public void LoadMap()
    {
        if (mapData != null && mapData.mapPrefab != null)
        {
            Vector3 mapPosition = new Vector3(0, 0, 0); // Đặt vị trí cụ thể
            GameObject mapInstance = Instantiate(mapData.mapPrefab, mapPosition, Quaternion.identity);

            // Kiểm tra xem mapInstance có collider không
            if (mapInstance.GetComponent<Collider>() == null)
            {
                Debug.LogError("Map prefab does not have a collider!");
            }
        }
    }
    // Hàm tạo bot
    public void SpawnBots()
    {
        for (int i = 0; i < numberOfBots; i++)
        {
            // Tạo bot tại các vị trí ngẫu nhiên trên map
            Vector3 botPosition = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
            Instantiate(botPrefab, botPosition, Quaternion.identity);
        }
    }

    public void LoadPlayer()
    {
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity); // Tạo nhân vật
        }
    }

   
    private void EndGame()
    {
        gameActive = false; // Dừng trò chơi
        Debug.Log("Trò chơi kết thúc!");
        // Hiển thị điểm số hoặc màn hình kết thúc ở đây
        // Thoát khỏi scene hiện tại và trở về scene chính
        restartButton.gameObject.SetActive(true); // Hiển thị nút Restart
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Tải lại scene hiện tại
    }
}
