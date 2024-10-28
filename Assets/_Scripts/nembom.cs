using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nembom : MonoBehaviour
{
    public GameObject bombPrefab;         // Prefab của bom
    public Transform throwPoint;          // Vị trí ném bom (tay nhân vật hoặc vị trí gần nhân vật)
    public float throwForce = 10f;        // Lực ném bom
    public float offset = 1f;             // Khoảng cách ném bom cách nhân vật
    private PlayerController playerController;    // Biến theo dõi hướng của nhân vật

    void Update()
    {
        // Kiểm tra nếu người chơi nhấn chuột trái để ném bom
        if (Input.GetKeyDown(KeyCode.E))
        {
            ThrowBomb();
        }
      
    }
    public void OnThrowBombButtonClicked()
    {
        Debug.Log("Ném bom!");
        // Thêm logic ném bom ở đây
        ThrowBomb();
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
}
