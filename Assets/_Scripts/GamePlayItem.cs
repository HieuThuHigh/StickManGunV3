using System;
using _GunMayHem.Gameplay;
using GameTool.Assistants.DesignPattern;
using GameToolSample.GameDataScripts.Scripts;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class GamePlayItem : MonoBehaviour
{
    [SerializeField] private Button freezeButton;
    [SerializeField] private Button cancleButton;
    [SerializeField] private Button testButton;
    [SerializeField] private GameObject addFreezePopup;
    [SerializeField] private GameObject plusPopup;
    [SerializeField] private GameObject quatityPopup;
    [SerializeField] private TextMeshProUGUI freezeText;
    [SerializeField] private CharacterControl characterControl;

    private int previousFreezeValue = -1; // Lưu giá trị trước đó để kiểm tra thay đổi

    private void Start()
    {
        freezeButton.onClick.AddListener(OnFreezeButtonClicked);
        cancleButton.onClick.AddListener(CancleEvent);
        testButton.onClick.AddListener(TestEvent);
    }

    private void TestEvent()
    {
        GameData.Freeze += 1;
    }


    private void Update()
    {
        // Luôn cập nhật text hiển thị
        freezeText.text = GameData.Freeze.ToString();

        // Chỉ thực hiện logic bật/tắt popup khi giá trị thay đổi
        if (GameData.Freeze != previousFreezeValue)
        {
            UpdatePopupStates(GameData.Freeze);
            previousFreezeValue = GameData.Freeze; // Cập nhật giá trị trước đó
        }

        if (GameData.Freeze < 0)
        {
            GameData.Freeze = 0;
        }
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
        }
    }

    private void CancleEvent()
    {
        Time.timeScale = 1;
        addFreezePopup.SetActive(false);
    }
}