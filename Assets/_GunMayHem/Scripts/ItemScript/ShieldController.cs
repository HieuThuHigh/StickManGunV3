using GameTool.Assistants.DesignPattern;
using GameToolSample.GameDataScripts.Scripts;
using GameToolSample.Scripts.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private Button shieldButton;
    [SerializeField] private GameObject addShieldPopup;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private GameObject plusPopupShield;
    [SerializeField] private GameObject quatityPopupShield;
    private int _previousShieldValue = -1;
    
    private void Start()
    {
        UpdateUI();
        shieldButton.onClick.AddListener(OnFreezeButtonClicked);
    }
    
    private void OnFreezeButtonClicked()
    {
        if (GameData.Shield == 0)
        {
            addShieldPopup.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            GameData.Shield -= 1;
            this.PostEvent(EventID.Shield);
        }

        Debug.LogError("check bang");
        UpdateUI();
    }

    void UpdateUI()
    {
        shieldText.text = GameData.Shield.ToString();
        if (GameData.Shield != _previousShieldValue)
        {
            UpdatePopupStates(GameData.Shield);
            _previousShieldValue = GameData.Shield;
        }

        if (GameData.Shield < 0) GameData.Shield = 0;
        if (GameData.Shield > 100) GameData.Shield = 100;
    }
    private void UpdatePopupStates(int freezeValue)
    {
        // Bật hoặc tắt popup dựa trên giá trị freezeValue
        if (freezeValue == 0)
        {
            plusPopupShield.SetActive(true);
            addShieldPopup.SetActive(false); // Popup addFreeze chỉ bật khi bấm nút
            quatityPopupShield.SetActive(false);
        }
        else
        {
            plusPopupShield.SetActive(false);
            quatityPopupShield.SetActive(true);
        }
    }
}
