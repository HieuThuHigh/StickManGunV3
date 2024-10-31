using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Header("Button Prefabs")]
    public Button jumpButtonPrefab;
    public Button moveLeftButtonPrefab;
    public Button moveRightButtonPrefab;
    public Button jumpDownButtonPrefab;
    public Button shootButtonPrefab; // Nút bắn súng
    public Button throwBombButtonPrefab; // Nút ném bom


    private Button jumpButton;
    private Button moveLeftButton;
    private Button moveRightButton;
    private Button jumpDownButton;
    private Button shootButton;
    private Button throwBombButton;

    [Header("Player Controller")]
    private  PlayerController playerController;
    private Nembom nembom;
    private GunController gunController;

    private void Start()
    {
        // Lấy PlayerController, Nembom và GunController từ GameObject cha hoặc xác định vị trí khác
        playerController = GetComponent<PlayerController>();
        nembom = GetComponent<Nembom>();
        gunController = GetComponent<GunController>();

        // Khởi tạo và gán sự kiện cho các nút
        jumpButton = Instantiate(jumpButtonPrefab, transform);
        SetButtonPosition(jumpButton.GetComponent<RectTransform>(), new Vector2(-200, 0)); // Vị trí nút nhảy
        jumpButton.onClick.AddListener(playerController.OnJumpButtonClicked);

        moveLeftButton = Instantiate(moveLeftButtonPrefab, transform);
        SetButtonPosition(moveLeftButton.GetComponent<RectTransform>(), new Vector2(-300, 0)); // Vị trí nút đi trái
        moveLeftButton.onClick.AddListener(playerController.OnMoveLeftButtonClicked);

        moveRightButton = Instantiate(moveRightButtonPrefab, transform);
        SetButtonPosition(moveRightButton.GetComponent<RectTransform>(), new Vector2(-100, 0)); // Vị trí nút đi phải
        moveRightButton.onClick.AddListener(playerController.OnMoveRightButtonClicked);

        jumpDownButton = Instantiate(jumpDownButtonPrefab, transform);
        SetButtonPosition(jumpDownButton.GetComponent<RectTransform>(), new Vector2(0, 0)); // Vị trí nút nhảy xuống
        jumpDownButton.onClick.AddListener(playerController.OnJumpDownButtonClicked);

        // Khởi tạo nút bắn súng và gán sự kiện
        shootButton = Instantiate(shootButtonPrefab, transform);
        SetButtonPosition(shootButton.GetComponent<RectTransform>(), new Vector2(200, -100)); // Vị trí nút bắn
        shootButton.onClick.AddListener(gunController.OnShootButtonClicked);

        // Khởi tạo nút ném bom và gán sự kiện
        throwBombButton = Instantiate(throwBombButtonPrefab, transform);
        SetButtonPosition(throwBombButton.GetComponent<RectTransform>(), new Vector2(300, -100)); // Vị trí nút ném bom
        throwBombButton.onClick.AddListener(nembom.OnThrowBombButtonClicked);
    }
    private void SetButtonPosition(RectTransform buttonRectTransform, Vector2 position)
    {
        buttonRectTransform.anchoredPosition = position; // Gán vị trí cho nút
        buttonRectTransform.sizeDelta = new Vector2(100, 100); // Kích thước nút
    }
}
