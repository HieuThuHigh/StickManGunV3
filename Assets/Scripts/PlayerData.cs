using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Player Data", order = 1)]
public class PlayerData : ScriptableObject
{
    public string characterName = "New Character"; // Tên nhân vật (có giá trị mặc định)
    public int health ;                      // Máu của nhân vật
    public float moveSpeed ;                  // Tốc độ di chuyển
    public float jumpForce  ;                  // Lực nhảy
    public int maxJumps ;                      // Số lần nhảy tối đa (mặc định là 1)
    public GunData GunData;                       // Vũ khí hiện tại của nhân vật

    // Constructor không cần thiết trong ScriptableObject, bạn có thể khởi tạo giá trị trực tiếp.
    // Nếu bạn muốn khởi tạo thông số từ code, bạn có thể tạo một phương thức để thay đổi dữ liệu.

    public void Initialize(string name, int health, float moveSpeed, float jumpForce, int maxJumps, GunData gun)
    {
        characterName = name;
        this.health = health;
        this.moveSpeed = moveSpeed;
        this.jumpForce = jumpForce;
        this.maxJumps = maxJumps;
        this.GunData = gun;
    }
}
