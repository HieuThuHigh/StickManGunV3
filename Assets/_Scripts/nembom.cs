using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Để sử dụng UI Text

public class Nembom : MonoBehaviour
{
    public GameObject bombPrefab;         // Prefab của bom
    public Transform throwPoint;          // Vị trí ném bom (tay nhân vật hoặc vị trí gần nhân vật)
    public float throwForce = 10f;        // Lực ném bom
    public float offset = 1f;             // Khoảng cách ném bom cách nhân vật
    private PlayerController playerController;    // Biến theo dõi hướng của nhân vật
    public int maxBombs;  // Số bom tối đa
    private int currentBombs;  // Số bom hiện tại
    public Text bombCountText; // UI để hiển thị số bom còn lại

    void Start()
    {
        currentBombs = maxBombs;  // Khởi tạo số bom
        UpdateBombCountUI();  // Cập nhật UI
        // Tìm kiếm component PlayerController trên đối tượng của nhân vật
        playerController = GetComponent<PlayerController>();

        // Kiểm tra xem playerController có null không, nếu có, in ra cảnh báo
        if (playerController == null)
        {
            Debug.LogWarning("Không tìm thấy PlayerController trên đối tượng.");
        }
    }

    void Update()
    {
        // Bạn có thể sử dụng OnThrowBombButtonClicked() để gọi ném bom từ UI
    }

    public void OnThrowBombButtonClicked()
    {
        Debug.Log("Ném bom!");
        // Thêm logic ném bom ở đây
        if (currentBombs > 0)  // Kiểm tra nếu còn bom
        {
            ThrowBomb();
            currentBombs--;  // Giảm số bom còn lại
            UpdateBombCountUI();  // Cập nhật UI
        }
        else
        {
            Debug.Log("Không còn bom để ném!");
        }
    }

    void ThrowBomb()
    {
        // Tính toán vị trí ném bom cách nhân vật 1 đơn vị theo hướng của nhân vật
        Vector3 bombStartPosition;
        if (playerController.isFacingRight)
        {
            bombStartPosition = throwPoint.position + new Vector3(offset, 0, 0); // Cách 1 đơn vị sang phải
        }
        else
        {
            bombStartPosition = throwPoint.position + new Vector3(-offset, 0, 0); // Cách 1 đơn vị sang trái
        }

        // Tạo bom từ Prefab tại vị trí đã tính toán
        GameObject bomb = Instantiate(bombPrefab, bombStartPosition, throwPoint.rotation);

        // Xoay bom theo hướng của nhân vật
        if (playerController.isFacingRight)
        {
            bomb.transform.rotation = Quaternion.Euler(0, 0, 0); // Nếu nhân vật quay phải, bom không xoay
        }
        else
        {
            bomb.transform.rotation = Quaternion.Euler(0, 180f, 0); // Nếu nhân vật quay trái, xoay bom 180 độ
        }

        // Lấy Rigidbody2D từ bom để thêm lực ném
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();

        // Tính toán hướng ném dựa trên hướng nhân vật
        Vector2 throwDirection = playerController.isFacingRight ? Vector2.right : Vector2.left;

        // Áp dụng lực ném theo hướng nhân vật (phải hoặc trái)
        rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
    }

    void UpdateBombCountUI()
    {
        if (bombCountText != null)
        {
            bombCountText.text = currentBombs.ToString(); // Cập nhật số bom còn lại trên UI
        }
    }
}
