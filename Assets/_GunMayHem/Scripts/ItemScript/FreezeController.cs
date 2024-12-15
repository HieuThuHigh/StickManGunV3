using GameTool.Assistants.DesignPattern;
using GameToolSample.GameDataScripts.Scripts;
using GameToolSample.Scripts.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FreezeController : MonoBehaviour
{
    [SerializeField] private Button freezeButton;
    [SerializeField] private GameObject addFreezePopup;
    [SerializeField] private GameObject plusPopup;
    [SerializeField] private GameObject quatityPopup;
    [SerializeField] private TextMeshProUGUI freezeText;
    private int _previousFreezeValue = -1; // Lưu giá trị trước đó để kiểm tra thay đổi

    [SerializeField] private Button testButton;
    [SerializeField] private Button cancelButton;

    private void Start()
    {
        UpdateUI();
        freezeButton.onClick.AddListener(OnFreezeButtonClicked);
        testButton.onClick.AddListener(TestEvent);
        cancelButton.onClick.AddListener(CancleEvent);
    }

    private void TestEvent()
    {
        GameData.Freeze += 1;
        UpdateUI();
    }

    private void CancleEvent()
    {
        Time.timeScale = 1;
        addFreezePopup.SetActive(false);
    }

    private void OnFreezeButtonClicked()
    {
        if (GameData.Freeze == 0)
        {
            addFreezePopup.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            GameData.Freeze -= 1;
            this.PostEvent(EventID.Freeze);
        }

        Debug.LogError("check bang");
        UpdateUI();
    }

    void UpdateUI()
    {
        freezeText.text = GameData.Freeze.ToString();
        if (GameData.Freeze != _previousFreezeValue)
        {
            UpdatePopupStates(GameData.Freeze);
            _previousFreezeValue = GameData.Freeze;
        }

        if (GameData.Freeze < 0) GameData.Freeze = 0;
        if (GameData.Freeze > 100) GameData.Freeze = 100;
    }
    private void UpdatePopupStates(int freezeValue)
    {
        // Bật hoặc tắt popup dựa trên giá trị freezeValue
        if (freezeValue == 0)
        {
            plusPopup.SetActive(true);
            addFreezePopup.SetActive(false); // Popup addFreeze chỉ bật khi bấm nút
            quatityPopup.SetActive(false);
        }
        else
        {
            plusPopup.SetActive(false);
            quatityPopup.SetActive(true);
        }
    }
}