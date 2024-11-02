using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData; // Biến để tham chiếu đến PlayerData
    [SerializeField] private MapData mapData;       // Biến để tham chiếu đến MapData
    [SerializeField] private GameObject playerPrefab; // Biến để tham chiếu đến prefab player
    [SerializeField] private GameObject botPrefab; // Tham chiếu đến prefab bot
    [SerializeField] private int numberOfBots = 2; // Số lượng bot muốn thêm


    private void Start()
    {
        LoadMap(); // Gọi LoadMap trong Start
        LoadPlayer(); // Tải nhân vật vào scene
        SpawnBots();    // Gọi hàm tạo bot
        CreateButtons(); // Tạo các nút trên màn hình
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
    // Hàm tạo và hiển thị các nút
    public void CreateButtons()
    {
        // Tạo một Canvas nếu chưa có
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObject = new GameObject("Canvas");
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
        }

    
    }

    private void CreateButton(Button buttonPrefab, string buttonText, Vector2 position)
    {
        if (buttonPrefab != null)
        {
            Button buttonInstance = Instantiate(buttonPrefab);
            buttonInstance.transform.SetParent(FindObjectOfType<Canvas>().transform, false); // Đặt parent là Canvas
            buttonInstance.GetComponent<RectTransform>().anchoredPosition = position; // Đặt vị trí nút
            buttonInstance.GetComponentInChildren<Text>().text = buttonText; // Đặt chữ cho nút
        }
        else
        {
            Debug.LogError($"Button prefab {buttonText} is not assigned!");
        }
    }
}
