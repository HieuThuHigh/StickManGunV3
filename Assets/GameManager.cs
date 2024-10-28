using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData; // Biến để tham chiếu đến PlayerData
    [SerializeField] private MapData mapData;       // Biến để tham chiếu đến MapData
    [SerializeField] private GameObject playerPrefab; // Biến để tham chiếu đến prefab player

    private void Start()
    {
        LoadMap(); // Gọi LoadMap trong Start
        LoadPlayer(); // Tải nhân vật vào scene
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


    public void LoadPlayer()
    {
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity); // Tạo nhân vật
        }
    }
}
