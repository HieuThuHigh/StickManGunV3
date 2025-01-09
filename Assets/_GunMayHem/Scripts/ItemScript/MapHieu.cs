using UnityEngine;

public class MapHieu : MonoBehaviour
{
    [SerializeField] private GameObject[] players; // Danh sách các Player trong scene Gameplay

    private void Start()
    {
        // Lấy chỉ số Player đã được chọn từ PlayerPrefs
        int selectedPlayerIndex = PlayerPrefs.GetInt("SelectedPlayer", -1);

        if (selectedPlayerIndex >= 0 && selectedPlayerIndex < players.Length)
        {
            // Bật map tương ứng
            for (int i = 0; i < players.Length; i++)
            {
                players[i].SetActive(i == selectedPlayerIndex);
            }
        }
        else
        {
            Debug.LogError("No player selected or index out of range!");
        }
    }
}
