using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public const string PLAYER_READY = "IsPlayerReady";
    public const string PLAYER_LOADED_LEVEL = "PlayerLoadedLevel";
    public PlayerData playerData;      // Tham chiếu tới dữ liệu nhân vật
    public Text characterNameText;
    public bool isFacingRight = true;  // Kiểm tra hướng của nhân vật
    private Rigidbody2D _rb;           // Rigidbody của nhân vật
    private bool _isGrounded;          // Kiểm tra xem nhân vật có chạm đất không
    private int _jumpCount = 0;        // Biến đếm số lần nhảy
    private GunController _gunController; // Điều khiển súng
    private bool moveLeft = false;
    private bool moveRight = false;
    private Vector2 _initialPosition; // Lưu trữ vị trí ban đầu của nhân vật

    public float jumpForce = 10f;  // Lực nhảy của nhân vật
    public float groundCheckDistance = 0.2f;  // Khoảng cách raycast kiểm tra mặt đất
    public float dropDownCooldown = 0.5f; // Thời gian cooldown cho việc rơi xuống
    public Transform raycastOrigin; // Tham chiếu tới đối tượng tùy chỉnh cho raycast
    public Transform raycastOrigin1; // Tham chiếu tới đối tượng tùy chỉnh cho raycast

    private float _dropDownTimer = 0f; // Timer để quản lý cooldown rơi xuống
    private Collider2D _collider;  // Collider của nhân vật
    public PhotonView photonView;
    public GameObject controlButtons; // Tham chiếu tới GameObject chứa nút bấm

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        // Kiểm tra nếu không phải là nhân vật của người chơi, ẩn các nút bấm
        if (!photonView.IsMine)
        {
            controlButtons.SetActive(false);
        }
        else
        {
            controlButtons.SetActive(true);
        }
        if (photonView.IsMine)
        {
            photonView.RPC("SyncFlip", RpcTarget.OthersBuffered, isFacingRight);
        }
        // Lấy thành phần Rigidbody2D của nhân vật
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>(); // Lấy collider
        // Lưu trữ vị trí ban đầu của nhân vật
        _initialPosition = transform.position;
        // Khởi tạo dữ liệu nhân vật với giá trị mặc định
        if (playerData == null)
        {
            playerData = ScriptableObject.CreateInstance<PlayerData>(); // Tạo instance mới của PlayerData
        }
        characterNameText.text = playerData.characterName;
        // In ra tên và máu của nhân vật
        Debug.Log("Nhân vật: " + playerData.characterName + ", Máu: " + playerData.health);
    }

    void Update()
    {
        // Kiểm tra xem người chơi có sở hữu nhân vật này không
        if (!photonView.IsMine) return;

        // Kiểm tra nếu người chơi nhấn phím W để nhảy (chỉ nhảy khi đang chạm đất hoặc đã nhảy ít nhất 1 lần)
        CheckGroundStatus();

        if (Input.GetKeyDown(KeyCode.W) && (_isGrounded || _jumpCount < 2))
        {
            Jump();
        }

        // Xử lý di chuyển nhân vật
        if (moveLeft)
        {
            transform.Translate(Vector3.left * playerData.moveSpeed * Time.deltaTime);
            Flip();
        }
        else if (moveRight)
        {
            transform.Translate(Vector3.right * playerData.moveSpeed * Time.deltaTime);
            Flip();
        }



        // Kiểm tra xem nhân vật có còn máu không
        CheckHealth();//-strong/-heart:>:o:-((:-h // Kiểm tra nếu người chơi nhấn phím S để rơi xuống (DropDown)
        if (Input.GetKeyDown(KeyCode.S) && _dropDownTimer <= 0f)
        {
            DropDown();
        }

        // Giảm thời gian cooldown cho việc rơi xuống
        _dropDownTimer -= Time.deltaTime;
    }
    [PunRPC]
    private void SyncPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }
    private void FixedUpdate()
    {
        // Đồng bộ vị trí trong FixedUpdate
        if (photonView.IsMine)
        {
            photonView.RPC("SyncPosition", RpcTarget.Others, transform.position, transform.rotation);
            photonView.RPC("SyncFlip", RpcTarget.Others, isFacingRight);
        }
    }

    public void UpdateGunData(GunData newGunData)
    {
        playerData.GunData = newGunData; // Cập nhật GunData trong PlayerData
    }

    public void StartMoveLeft()
    {
        if (photonView.IsMine)
            photonView.RPC("SetMoveLeft", RpcTarget.AllBuffered, true);
    }

    public void StopMoveLeft()
    {
        if (photonView.IsMine)
            photonView.RPC("SetMoveLeft", RpcTarget.AllBuffered, false);
    }
    [PunRPC]
    private void SetMoveLeft(bool state)
    {
        moveLeft = state;
    }

    public void StartMoveRight()
    {
        if (photonView.IsMine)
            photonView.RPC("SetMoveRight", RpcTarget.AllBuffered, true);
    }

    public void StopMoveRight()
    {
        if (photonView.IsMine)
            photonView.RPC("SetMoveRight", RpcTarget.AllBuffered, false);
    }
    [PunRPC]
    private void SetMoveRight(bool state)
    {
        moveRight = state;
    }

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

    public void Jump()
    {
        if (_isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _isGrounded = false;
            _jumpCount = 1;
            Debug.Log("Nhân vật đã nhảy!");
        }
        else if (_jumpCount < 1)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _jumpCount++;
            Debug.Log("Nhân vật thực hiện double jump!");
        }
        else
        {
            Debug.Log("Nhân vật không thể nhảy vì đã thực hiện đủ số lần nhảy.");
        }
    }

    void CheckHealth()
    {
        if (playerData.health <= 0)
        {
            Debug.Log("Nhân vật đã hết máu!");
        }
    }
    [PunRPC]
    private void SyncFlip(bool facingRight)
    {
        isFacingRight = facingRight;

        // Cập nhật hướng nhân vật dựa trên trạng thái facingRight
        Vector3 scaler = transform.localScale;
        scaler.x = Mathf.Abs(scaler.x) * (facingRight ? 1 : -1);
        transform.localScale = scaler;
    }
    void Flip()
    {
        if (moveLeft && isFacingRight)
        {
            isFacingRight = false;
            photonView.RPC("SyncFlip", RpcTarget.Others, isFacingRight);
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        else if (moveRight && !isFacingRight)
        {
            isFacingRight = true;
            photonView.RPC("SyncFlip", RpcTarget.Others, isFacingRight);
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }

    private void CheckGroundStatus()
    {
        if (raycastOrigin != null)
        {
            RaycastHit2D hitDown = Physics2D.Raycast(raycastOrigin.position, Vector2.down, groundCheckDistance);

            if (hitDown.collider != null)
            {
                _isGrounded = true;//-strong/-heart:>:o:-((:-h _jumpCount = 0;
            }
            else
            {
                _isGrounded = false;
            }

            Debug.DrawRay(raycastOrigin.position, Vector2.down * groundCheckDistance, Color.red);
        }
        else
        {
            Debug.LogWarning("raycastOrigin chưa được chỉ định!");
        }
    }

    private void DropDown()
    {
        if (raycastOrigin1 != null)
        {
            RaycastHit2D hitDown1 = Physics2D.Raycast(raycastOrigin1.position, Vector2.down, 2f);
            if (hitDown1)
            {
                // Tắt collider tạm thời
                _collider.enabled = false;

                // Đặt lại timer để kiểm soát thời gian cooldown
                _dropDownTimer = dropDownCooldown;

                // Bật lại collider sau một thời gian
                StartCoroutine(ReEnableCollider());
            }
            Debug.DrawRay(raycastOrigin1.position, Vector2.down * groundCheckDistance, Color.blue);
        }
    }

    private IEnumerator ReEnableCollider()
    {
        // Đợi một khoảng thời gian nhỏ (tùy theo cooldown)
        yield return new WaitForSeconds(0.2f);

        // Bật lại collider
        _collider.enabled = true;

        Debug.Log("Collider đã được bật lại.");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm với tường
        if (collision.gameObject.CompareTag("wall"))
        {
            // Di chuyển về vị trí ban đầu khi va chạm với tường
            transform.position = _initialPosition;
            Debug.Log("Nhân vật đã va chạm với tường và quay về vị trí ban đầu.");
        }
        // Kiểm tra va chạm với nền tảng để nhận diện khi nhân vật chạm đất
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _jumpCount = 0; // Reset lại số lần nhảy khi chạm đất
            Debug.Log("Nhân vật đã chạm đất.");
        }
    }

    // Kiểm tra khi va chạm với các vật thể khác
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Đảm bảo nhân vật vẫn ở trên mặt đất trong khi tiếp xúc với mặt đất
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Khi không còn tiếp xúc với nền tảng, đánh dấu là không còn chạm đất
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
            Debug.Log("Nhân vật đã rời khỏi mặt đất.");
        }
    }

    // Xử lý khi va chạm với vật thể (sự kiện bắt đầu khi va chạm)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))//-strong/-heart:>:o:-((:-h {
            // Xử lý khi nhân vật thu thập vật phẩm
            Debug.Log("Nhân vật đã thu thập vật phẩm!");
        Destroy(other.gameObject); // Xóa vật phẩm sau khi thu thập
    }
    // Hàm để khởi tạo và cấu hình các đối tượng khi bắt đầu trò chơi
    private void ResetCharacter()
    {
        transform.position = _initialPosition; // Đặt lại vị trí ban đầu cho nhân vật
        _rb.velocity = Vector2.zero;  // Reset vận tốc về 0 để ngừng mọi chuyển động
        _isGrounded = true; // Đánh dấu nhân vật đã chạm đất
        _jumpCount = 0;  // Đặt lại số lần nhảy về 0
        Debug.Log("Nhân vật đã được reset về vị trí ban đầu.");
    }
}
