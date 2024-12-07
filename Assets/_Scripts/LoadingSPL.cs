using System.Collections;
using GameTool.Assistants.DesignPattern;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingSPL : SingletonMonoBehaviour<LoadingSPL>
{
    public GameObject[] popups;
    public Image loadingImage; 
    public TextMeshProUGUI loadingText; // Text để hiển thị phần trăm loading
    [SerializeField] private GameObject startGamePopup;
    [SerializeField] private GameObject loadingGamePopup;
    public GameObject homePopup;
    [SerializeField] private Button startGameBtn;
    [SerializeField] private float fillDuration = 4.5f; 
    [SerializeField] private float currentFillTime = 0f;
    public GameObject popupLoading;

    private void Start()
    {
        startGameBtn.onClick.AddListener(ButtonClick);
        StartCoroutine(HandleLoadingPopups());
        loadingImage.fillAmount = 0f; // Đảm bảo thanh tiến trình bắt đầu từ 0
        loadingText.text = "0%"; // Khởi tạo text loading
    }

    private IEnumerator HandleLoadingPopups()
    {
        for (int i = 0; i < 3; i++) // Chỉnh sửa 3.5 thành 3
        {
            for (int j = 0; j < popups.Length; j++)
            {
                popups[j].SetActive(true);
                yield return new WaitForSeconds(0.5f);
                // Ẩn popup ngay sau đó
                popups[j].SetActive(false);
            }
        }

        Debug.Log("Hoàn tất quá trình loading");
    }

    private void Update()
    {
        Event(); // Gọi hàm Event trong Update để đảm bảo cập nhật liên tục
    }

    void Event()
    {
        if (loadingImage.fillAmount < 1f)
        {
            // Tăng thời gian lấp đầy theo thời gian thực
            currentFillTime += Time.deltaTime;

            // Tính tỷ lệ đã lấp đầy dựa trên thời gian hiện tại và tổng thời gian
            float progress = Mathf.Clamp01(currentFillTime / fillDuration);
            loadingImage.fillAmount = progress;

            // Cập nhật text loading (phần trăm)
            loadingText.text = Mathf.RoundToInt(progress * 100f) + "%";
        }
        else if (loadingImage.fillAmount >= 1f && popupLoading.activeSelf) // Kiểm tra thanh đã đầy và popup chưa tắt
        {
            // Khi loading đầy 100%, tắt popupLoading và hiện startGamePopup
            popupLoading.SetActive(false);
            loadingImage.gameObject.SetActive(false); // Tắt loading image
            loadingText.gameObject.SetActive(false); // Tắt loading text
            startGamePopup.SetActive(true); // Hiện startGamePopup
        }
    }

    void ButtonClick()
    {
        loadingGamePopup.SetActive(false);
        homePopup.SetActive(true);
    }
}
