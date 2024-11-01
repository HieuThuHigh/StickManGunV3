using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class test : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float speed = 5f;
    private bool moveLeft = false;
    private bool moveRight = false;
    private Rigidbody2D rb;
    private bool isfacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (moveRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpDown();
        }
    }
    // Hàm kích hoạt di chuyển trái
    public void StartMoveLeft()
    {
        moveLeft = true;
    }

    // Hàm dừng di chuyển trái
    public void StopMoveLeft()
    {
        moveLeft = false;
    }

    // Hàm kích hoạt di chuyển phải
    public void StartMoveRight()
    {
        moveRight = true;
    }

    // Hàm dừng di chuyển phải
    public void StopMoveRight()
    {
        moveRight = false;
    }
     // Xử lý sự kiện khi nhấn nút
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "LeftButton")
        {
            StartMoveLeft();
        }
        else if (eventData.pointerPress.name == "RightButton")
        {
            StartMoveRight();
        }
    }

    // Xử lý sự kiện khi thả nút
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerPress.name == "LeftButton")
        {
            StopMoveLeft();
        }
        else if (eventData.pointerPress.name == "RightButton")
        {
            StopMoveRight();
        }
    }
    public void JumpDown()
    {
        RaycastHit2D hitDown;
        float rayDistanceDown = 0.2f; // Khoảng cách ray để kiểm tra collider bên dưới
        Vector2 rayOrigin = transform.position;

        // Dùng raycast để kiểm tra collider ở dưới
        hitDown = Physics2D.Raycast(rayOrigin, Vector2.down, rayDistanceDown);
        if (hitDown.collider != null)
        {
            // Di chuyển player xuống vị trí đáy của collider phía dưới
            float newYPosition = hitDown.collider.bounds.min.y - 0.2f;
            transform.position = new Vector2(transform.position.x, newYPosition);
            Debug.Log("Nhân vật đã nhảy xuống một lớp collider: " + hitDown.collider.name);
        }
        else
        {
            Debug.Log("Không còn lớp collider ở dưới.");
        }
    }
    public void Jump()
    {
        RaycastHit2D hitUp;
        float rayDistanceUp = 1f; // Khoảng cách ray ngắn hơn để chỉ phát hiện collider gần nhất

        Vector2 rayOrigin = transform.position;

        // Di chuyển lên khi nhấn phím W

        // Dùng raycast để kiểm tra collider ở trên, tính từ vị trí hiện tại
        hitUp = Physics2D.Raycast(rayOrigin, Vector2.up, rayDistanceUp);
        if (hitUp.collider != null)
        {
            // Di chuyển player lên vị trí đỉnh của collider phía trên
            transform.position = new Vector2(transform.position.x, hitUp.collider.bounds.max.y + 0.7f);
            Debug.Log("Di chuyển lên trên một lớp collider.");
        }
        else
        {
            // Không tìm thấy collider, không thể di chuyển lên
            Debug.Log("Không còn lớp collider ở trên.");
        }


        // Di chuyển xuống khi nhấn phím S

    }
   

}
