using UnityEngine;
using TMPro; // Thêm thư viện để sử dụng TMP_Text

public class namechange : MonoBehaviour
{
    [Header("Input Field & Text UI Elements")]
    public TMP_InputField playerNameInputField; // Nhập tên người chơi
    public TMP_Text playerNameText;             // Hiển thị tên người chơi (TMP_Text)

    void Start()
    {
        // Lấy giá trị từ PlayerPrefs và hiển thị lên Text UI
        if (playerNameText != null)
        {
            string playerName = PlayerPrefs.GetString("PlayerName", "Player");
            playerNameText.text = playerName; // Cập nhật tên lên UI
            Debug.Log("Tên người chơi lấy được: " + playerName);
        }
        else
        {
            Debug.LogError("playerNameText chưa được gán trong Inspector!");
        }

        // Đảm bảo InputField chứa tên người chơi đã lưu (nếu có)
        if (playerNameInputField != null)
        {
            playerNameInputField.text = PlayerPrefs.GetString("PlayerName", ""); // Đặt tên đã lưu vào InputField
        }
        else
        {
            Debug.LogError("playerNameInputField chưa được gán trong Inspector!");
        }
    }

    public void SavePlayerName()
    {
        // Kiểm tra và lưu tên người chơi
        if (playerNameInputField != null)
        {
            string playerName = playerNameInputField.text;

            if (!string.IsNullOrEmpty(playerName)) // Kiểm tra nếu tên không rỗng
            {
                // Lưu vào PlayerPrefs
                PlayerPrefs.SetString("PlayerName", playerName);
                PlayerPrefs.Save();  // Đảm bảo dữ liệu được lưu
                Debug.Log("Tên người chơi đã được lưu: " + playerName);

                // Cập nhật Text UI hiển thị (nếu cần)
                if (playerNameText != null)
                {
                    playerNameText.text = playerName; // Cập nhật tên lên UI
                }
            }
            else
            {
                Debug.LogWarning("Tên người chơi không hợp lệ, vui lòng nhập lại!");
            }
        }
        else
        {
            Debug.LogError("playerNameInputField chưa được gán trong Inspector!");
        }
    }
}
