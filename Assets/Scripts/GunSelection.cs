using UnityEngine;

public class GunSelection : MonoBehaviour
{
    public GunController gunController; // Tham chiếu đến GunController

    public void SelectGun(int gunIndex)
    {
        gunController.ChangeGun(gunIndex); // Gọi hàm để thay đổi súng
        StartGame(); // Bắt đầu trò chơi
    }

    void StartGame()
    {
        // Chuyển đến cảnh chơi hoặc khởi động gameplay
        Debug.Log("Bắt đầu trò chơi với súng: " + gunController.gunData.gunName);
        // Ví dụ: SceneManager.LoadScene("GameScene");
    }
}
